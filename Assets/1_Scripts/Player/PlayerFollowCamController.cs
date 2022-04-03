using UnityEngine;
using Xezebo.Input;
using Zenject;

namespace Xezebo.Player
{
    public class PlayerFollowCamController : MonoBehaviour
    {
        
        [Inject] 
        PlayerInputBroadcaster _playerInputBroadcaster;
    
        [Header("CineMachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject cinemachineCameraTarget;

        [SerializeField] float yawSpeed = 1;
        [SerializeField] float pitchSpeed = 1;

        [Tooltip("How far in degrees can you move the camera up")]
        public float topClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float bottomClamp = -30.0f;

        [Tooltip("Additional degrees to override the camera. Useful for fine tuning camera position when locked")]
        public float cameraAngleOverride;

        [Tooltip("For locking the camera position on all axis")]
        public bool lockCameraPosition;

        // cinemachine
        private float cinemachineTargetYaw;
        private float cinemachineTargetPitch;
        private const float Threshold = 0.01f;

        // input
        Vector2 _lookInput;

        void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation() 
        {
            _lookInput = _playerInputBroadcaster.Look();
            // if there is an input and camera position is not fixed
            if (_lookInput.sqrMagnitude >= Threshold && !lockCameraPosition)
            {
                // cinemachineTargetYaw += _lookInput.x * Time.deltaTime * yawSpeed;
                // cinemachineTargetPitch += _lookInput.y * Time.deltaTime * pitchSpeed;
                
                cinemachineTargetYaw += _lookInput.x * yawSpeed;
                cinemachineTargetPitch += _lookInput.y * pitchSpeed;
            }

            // clamp our rotations so our values are limited 360 degrees
            cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

            // Cinemachine will follow this target
            cinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + cameraAngleOverride, cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
}

}