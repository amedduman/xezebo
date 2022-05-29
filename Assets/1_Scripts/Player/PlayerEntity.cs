using UnityEngine;
using Xezebo.DI;
using Xezebo.EntitiyValues;
using Xezebo.Equipment;
using Xezebo.Management;
using Zenject;

namespace Xezebo.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [Inject(Id = nameof(BindingIdentifiers.input_handler))]
        private InputHandler _inputHandler;
        [Inject] 
        ResourceHandler _resourceHandler;
        [Inject(Id = "main")]
        Camera _mainCam;
        [Inject(Id = "activeGun")]
        Gun _gun;
        
        public GameObject cinemachineCameraTarget;

        [SerializeField] private PlayerEntityValues _playerEntityValues;
        [SerializeField] PlayerAnimatorController playerAnimCtrl;
        [SerializeField] private CharacterController _charController;
        
        [Header("attack controller")]
        [SerializeField] LayerMask _layerMask;
        PlayerAttackController attackHandler;
        PlayerMovementController _movementController;
        PlayerFollowCamController _followCamController;
        
        void Awake()
        {
            attackHandler = new PlayerAttackController(_inputHandler, _mainCam, _gun, _layerMask, _resourceHandler);
            _movementController =
                new PlayerMovementController(_charController, _playerEntityValues, playerAnimCtrl, this, _mainCam,
                _inputHandler);
            _followCamController = new PlayerFollowCamController(_inputHandler, _playerEntityValues, cinemachineCameraTarget);

        }

        void OnEnable()
        {
            attackHandler.RegisterToInputEvents();
        }

        void OnDisable()
        {
            attackHandler.DeRegisterToInputEvents();
        }

        private void Update()
        {
            _movementController.Tick();
        }

        void LateUpdate()
        {
            _followCamController.CameraRotation();
        }
    }
}