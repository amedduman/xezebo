using UnityEngine;
using UnityEngine.InputSystem;
using Xezebo.Enemy;
using Xezebo.Equipment;
using Xezebo.Misc;
using Xezebo.Input;
using Xezebo.Managers;
using Zenject;

namespace Xezebo.Player
{
    public class PlayerAttackController
    {
        GameManager _gameManager;
        
        readonly Gun _gun;
        readonly Camera _mainCam;
        readonly PlayerInputBroadcaster _inputBroadcaster;
        readonly LayerMask _layer;

        
        public PlayerAttackController(PlayerInputBroadcaster inputBroadcaster,
            Camera cam, Gun gun, LayerMask layer, GameManager gameManager)
        {
            _inputBroadcaster = inputBroadcaster;
            _mainCam = cam;
            _gun = gun;
            _layer = layer;
            _gameManager = gameManager;
        }
        
        public void RegisterToInputEvents()
        {
            _inputBroadcaster.Fire().performed += Shoot;
        }

        public void DeRegisterToInputEvents()
        { 
            _inputBroadcaster.Fire().performed -= Shoot;
        }


        void Shoot(InputAction.CallbackContext Obj)
        {
            if (_gameManager == null)
            {
                Debug.Log("null");
            }
            if (!_gameManager.CanShoot()) return;

            RaycastHit rayCast = Raycaster.ShootRay(_mainCam.transform, _layer);
            if (rayCast.collider != null)
            {
                _gun.Fire(rayCast.point);
                if (rayCast.transform.TryGetComponent(out EnemySM enemy))
                {
                    enemy.GetDamage();
                }
            }
            // when it looks at the sky bullet goes forward 
            else
            {
                _gun.Fire(null);
            }
        }
    }
}