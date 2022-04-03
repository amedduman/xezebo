using UnityEngine;
using Xezebo.Equipment;
using Xezebo.Input;
using Zenject;

namespace Xezebo.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        PlayerAttackController attackHandler;
        [Inject]
        PlayerInputBroadcaster _inputBroadcaster;
        [Inject(Id = "main")]
        Camera _mainCam;
        [Inject(Id = "activeGun")]
        Gun _gun;
        
        void Awake()
        {
            attackHandler = new PlayerAttackController(_inputBroadcaster, _mainCam, _gun);
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