using UnityEngine;
using Xezebo.EntitiyValues;
using Xezebo.Management;

namespace Xezebo.Player
{
    public class PlayerFollowCamController
    {
        InputHandler _inputHandler;

        PlayerEntityValues _playerVals;    

        // cinemachine
        GameObject _camTarget;
        private float cinemachineTargetYaw;
        private float cinemachineTargetPitch;
        private const float Threshold = 0.01f;

        // input
        Vector2 _lookInput;
        
        public PlayerFollowCamController(InputHandler inputHandler, PlayerEntityValues playerVals, GameObject camTarget)
        {
            _inputHandler = inputHandler;
            _playerVals = playerVals;
            _camTarget = camTarget;
        }

        public void CameraRotation() 
        {
            _lookInput = new Vector2(_inputHandler.Look.x * 400, _inputHandler.Look.y * 100);
            
            // if there is an input and camera position is not fixed
            if (_lookInput.sqrMagnitude >= Threshold && !_playerVals.lockCameraPosition)
            {
                // cinemachineTargetYaw += _lookInput.x * Time.deltaTime * yawSpeed;
                // cinemachineTargetPitch += _lookInput.y * Time.deltaTime * pitchSpeed;
                
                cinemachineTargetYaw += _lookInput.x * _playerVals.yawSpeed;
                cinemachineTargetPitch += _lookInput.y * _playerVals.pitchSpeed;
            }

            // clamp our rotations so our values are limited 360 degrees
            cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, _playerVals.bottomClamp, _playerVals.topClamp);

            // Cinemachine will follow this target
            _camTarget.transform.rotation = 
            Quaternion.Euler(cinemachineTargetPitch + _playerVals.cameraAngleOverride, cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
}

}