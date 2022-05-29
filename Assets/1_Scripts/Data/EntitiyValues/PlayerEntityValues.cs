using UnityEngine;
using Xezebo.Player;

namespace Xezebo.EntitiyValues
{
    [CreateAssetMenu(fileName = "PlayerEntityValues", menuName = "SO/PlayerEntityValues", order = 0)]
    public class PlayerEntityValues : ScriptableObject
    {
        // movement 
        [Header(nameof(PlayerMovementController))]
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
        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;


        // follow cam
        [Header(nameof(PlayerFollowCamController))]
        public float yawSpeed = 1;
        public float pitchSpeed = 1;

        [Tooltip("How far in degrees can you move the camera up")]
        public float topClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float bottomClamp = -30.0f;

        [Tooltip("Additional degrees to override the camera. Useful for fine tuning camera position when locked")]
        public float cameraAngleOverride;

        [Tooltip("For locking the camera position on all axis")]
        public bool lockCameraPosition;
    }
}