using UnityEngine;
using UnityEngine.InputSystem;
using Xezebo.Enemy;
using Xezebo.Equipment;
using Xezebo.Misc;
using Xezebo.Input;

namespace Xezebo.Player
{
    public class PlayerAttackController
    {
        readonly Gun _gun;
        readonly Camera _mainCam;
        readonly PlayerInputBroadcaster _inputBroadcaster;
        readonly LayerMask _layer; 

        public PlayerAttackController(PlayerInputBroadcaster inputBroadcaster, Camera cam, Gun gun, LayerMask layer)
        {
            _inputBroadcaster = inputBroadcaster;
            _mainCam = cam;
            _gun = gun;
            _layer = layer;
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