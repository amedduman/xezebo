using Xezebo.EntitiyValues;
using UnityEngine;
using Xezebo.Management;

namespace Xezebo.Player
{
    
public class PlayerMovementController
{
	PlayerEntity _player;
	CharacterController _controller;
	PlayerAnimatorController _playerAnimCtrl;
	Camera _mainCam;
	InputHandler _inputHandler;
	PlayerEntityValues _playerEntityValues;

	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	bool Grounded = true;
	
	// timeout deltatime
	float _jumpTimeoutDelta;
	float _fallTimeoutDelta;
	
	// speed
	float _speed;
	float _animationBlend;
	float _targetRotation = 0.0f;
	float _rotationVelocity;
	float _verticalVelocity;
	float _terminalVelocity = 53.0f;
	
	public PlayerMovementController(CharacterController controller, PlayerEntityValues playerEntityValues,
		PlayerAnimatorController playerAnimCtrl, PlayerEntity player,
		Camera mainCam, InputHandler inputHandler)
	{
		_player = player;
		_controller = controller;
		_playerAnimCtrl = playerAnimCtrl;
		_mainCam = mainCam;
		_playerEntityValues = playerEntityValues;
		
		_jumpTimeoutDelta = _playerEntityValues.JumpTimeout;
		_fallTimeoutDelta = _playerEntityValues.FallTimeout;
		_inputHandler = inputHandler;
	}

	public void Tick()
    {
	    if (_playerEntityValues.ShooterMode)
	    {
		    RotatePlayer();
	    }
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
	        //bool jumpInput_ = _playerEntityValues._playerInputBroadcaster.Jump();
	        
	        bool jumpInput = _inputHandler.Jump;
            // reset the fall timeout timer
            _fallTimeoutDelta = _playerEntityValues.FallTimeout;
            
            // set animator
            _playerAnimCtrl.Jump(false);
            _playerAnimCtrl.FreeFall(false);

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (jumpInput && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(_playerEntityValues.JumpHeight * -2f * _playerEntityValues.Gravity);
                
	            // set jump animation
	            _playerAnimCtrl.Jump(true);
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = _playerEntityValues.JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
	            _playerAnimCtrl.FreeFall(true);
            }
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += _playerEntityValues.Gravity * Time.deltaTime;
        }
    }
    
    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(_player.transform.position.x, _player.transform.position.y - _playerEntityValues.GroundedOffset, _player.transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, _playerEntityValues.GroundedRadius, _playerEntityValues.GroundLayers, QueryTriggerInteraction.Ignore);
        
        _playerAnimCtrl.Ground(Grounded);
    }
	private void Move()
	{
		Vector2 moveInput = _inputHandler.Move;
		bool sprintInput_ = _inputHandler.Sprint;

		// set target speed based on move speed, sprint speed and if sprint is pressed
		float targetSpeed_ = sprintInput_ ? _playerEntityValues.SprintSpeed : _playerEntityValues.MoveSpeed;

		// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

		// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is no input, set the target speed to 0
		if (moveInput == Vector2.zero) targetSpeed_ = 0.0f;

		// a reference to the players current horizontal velocity
		float currentHorizontalSpeed_ = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

		float speedOffset_ = 0.1f;
		// float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

		// accelerate or decelerate to target speed
		if (currentHorizontalSpeed_ < targetSpeed_ - speedOffset_ || currentHorizontalSpeed_ > targetSpeed_ + speedOffset_)
		{
			// creates curved result rather than a linear one giving a more organic speed change
			// note T in Lerp is clamped, so we don't need to clamp our speed
			_speed = Mathf.Lerp(currentHorizontalSpeed_, targetSpeed_ /* inputMagnitude*/, Time.deltaTime * _playerEntityValues.SpeedChangeRate);

			// round speed to 3 decimal places
			_speed = Mathf.Round(_speed * 1000f) / 1000f;
		}
		else
		{
			_speed = targetSpeed_;
		}
		_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed_, Time.deltaTime * _playerEntityValues.SpeedChangeRate);

		// normalise input direction
		Vector3 inputDirection_ = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;
		
		// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is a move input rotate player when the player is moving
		if (moveInput != Vector2.zero)
		{
			_targetRotation = Mathf.Atan2(inputDirection_.x, inputDirection_.z) * Mathf.Rad2Deg + _mainCam.transform.eulerAngles.y;
			// float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

			// rotate to face input direction relative to camera position
			// transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		}

		Vector3 targetDirection_ = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
		
		// move the player
		Debug.DrawRay(_player.transform.position, targetDirection_);
		_controller.Move(targetDirection_.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

		// set animation
		_playerAnimCtrl.Move(_animationBlend, 1);
		_playerAnimCtrl.Strafe(moveInput.x, moveInput.y);
	}

	void RotatePlayer()
	{
		// Vector2 moveInput_ = PlayerInputBroadcaster.Instance.Move();
		// Vector3 inputDirection_ = new Vector3(moveInput_.x, 0.0f, moveInput_.y).normalized;
		//
		// _targetRotation = Mathf.Atan2(inputDirection_.x, inputDirection_.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
		// float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
		//
		// // rotate to face input direction relative to camera position
		// transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

		
		float yawCamera = _mainCam.transform.rotation.eulerAngles.y;
		float rotation = Mathf.SmoothDampAngle(_player.transform.eulerAngles.y, yawCamera, ref _rotationVelocity, _playerEntityValues.RotationSmoothTime);
		_player.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		// transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), 5 * Time.deltaTime);
	}
	
	private void OnDrawGizmosSelected()
	{
		Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
		Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

		if (Grounded) Gizmos.color = transparentGreen;
		else Gizmos.color = transparentRed;
			
		// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
		Gizmos.DrawSphere(new Vector3(_player.transform.position.x, _player.transform.position.y - _playerEntityValues.GroundedOffset, _player.transform.position.z), _playerEntityValues.GroundedRadius);
	}

}
}
