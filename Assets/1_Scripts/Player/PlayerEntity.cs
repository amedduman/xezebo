using UnityEngine;
using Xezebo.Equipment;
using Xezebo.Input;
using Xezebo.Managers;
using Zenject;

namespace Xezebo.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        public float MaxHealth = 100;
        
        [Inject] 
        PlayerInputBroadcaster _inputBroadcaster;
        [Inject] 
        GameManager _gameManager;
        [Inject(Id = "main")]
        Camera _mainCam;
        [Inject(Id = "activeGun")]
        Gun _gun;

        [SerializeField] LayerMask _layerMask;
        PlayerAttackController attackHandler;
        
        void Awake()
        {
            attackHandler = new PlayerAttackController(_inputBroadcaster, _mainCam, _gun, _layerMask, _gameManager);
        }

        void OnEnable()
        {
            attackHandler.RegisterToInputEvents();
        }

        void OnDisable()
        {
            attackHandler.DeRegisterToInputEvents();
        }
    }
}