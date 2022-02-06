using UnityEngine;

namespace Player
{
    
public class PlayerMovementController : MonoBehaviour
{
	CharacterController _controller;
	[SerializeField] GameObject mainCamera;
	[SerializeField] PlayerAnimatorController playerAnimCtrl;

	[Header("Player")]
	[Tooltip("Move speed of the character in m/s")]
	public float MoveSpeed = 2.0f;
	[Tooltip("Sprint speed of the character in m/s")]
	public float SprintSpeed = 5.335f;
	[Tooltip("How fast the character turns to face movement direction")]
	[Range(0.0f, 0.3f)]
	public float RotationSmoothTime = 0.12f;
	[Tooltip("Acceleration and deceleration")]
	public float SpeedChangeRate = 10.0f;
	public bool ShooterMode;

	[Space(10)]
	[Tooltip("The height the player can jump")]
	public float JumpHeight = 1.2f;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	public float Gravity = -15.0f;

	[Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float JumpTimeout = 0.50f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float FallTimeout = 0.15f;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	public bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	public float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	public float GroundedRadius = 0.28f;
	[Tooltip("What layers the character uses as ground")]
	public LayerMask GroundLayers;

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
	
	void Start()
	{
		_controller = GetComponent<CharacterController>();
		
		_jumpTimeoutDelta = JumpTimeout;
		_fallTimeoutDelta = FallTimeout;
	}

	private void Update()
    {
	    if (ShooterMode)
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
	        bool jumpInput_ = PlayerInputBroadcaster.Instance.Jump();
	        
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;
            
            // set animator
            playerAnimCtrl.Jump(false);
            playerAnimCtrl.FreeFall(false);

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (jumpInput_ && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                
	            // set jump animation
	            playerAnimCtrl.Jump(true);
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
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
	            playerAnimCtrl.FreeFall(true);
            }
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }
    
    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        
        playerAnimCtrl.Ground(Grounded);
    }
	private void Move()
	{
		Vector2 moveInput_ = PlayerInputBroadcaster.Instance.Move();
		bool sprintInput_ = PlayerInputBroadcaster.Instance.Sprint();

		// set target speed based on move speed, sprint speed and if sprint is pressed
		float targetSpeed_ = sprintInput_ ? SprintSpeed : MoveSpeed;

		// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

		// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is no input, set the target speed to 0
		if (moveInput_ == Vector2.zero) targetSpeed_ = 0.0f;

		// a reference to the players current horizontal velocity
		float currentHorizontalSpeed_ = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

		float speedOffset_ = 0.1f;
		// float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

		// accelerate or decelerate to target speed
		if (currentHorizontalSpeed_ < targetSpeed_ - speedOffset_ || currentHorizontalSpeed_ > targetSpeed_ + speedOffset_)
		{
			// creates curved result rather than a linear one giving a more organic speed change
			// note T in Lerp is clamped, so we don't need to clamp our speed
			_speed = Mathf.Lerp(currentHorizontalSpeed_, targetSpeed_ /* inputMagnitude*/, Time.deltaTime * SpeedChangeRate);

			// round speed to 3 decimal places
			_speed = Mathf.Round(_speed * 1000f) / 1000f;
		}
		else
		{
			_speed = targetSpeed_;
		}
		_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed_, Time.deltaTime * SpeedChangeRate);

		// normalise input direction
		Vector3 inputDirection_ = new Vector3(moveInput_.x, 0.0f, moveInput_.y).normalized;
		
		// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is a move input rotate player when the player is moving
		if (moveInput_ != Vector2.zero)
		{
			_targetRotation = Mathf.Atan2(inputDirection_.x, inputDirection_.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
			// float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

			// rotate to face input direction relative to camera position
			// transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		}

		Vector3 targetDirection_ = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
		
		// move the player
		Debug.DrawRay(transform.position, targetDirection_);
		_controller.Move(targetDirection_.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

		// set animation
		playerAnimCtrl.Move(_animationBlend, 1);
		playerAnimCtrl.Strafe(moveInput_.x, moveInput_.y);
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

		
		float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
		float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, yawCamera, ref _rotationVelocity, RotationSmoothTime);
		transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		// transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), 5 * Time.deltaTime);
	}
	
	private void OnDrawGizmosSelected()
	{
		Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
		Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

		if (Grounded) Gizmos.color = transparentGreen;
		else Gizmos.color = transparentRed;
			
		// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
	}

}
}
