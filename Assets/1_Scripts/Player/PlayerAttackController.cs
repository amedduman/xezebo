using UnityEngine;
using Xezebo.Enemy;
using Xezebo.Equipment;
using Xezebo.Misc;
using Xezebo.Management;

namespace Xezebo.Player
{
    public class PlayerAttackController
    {
        ResourceHandler _resourceHandler;

        readonly InputHandler _inputHandler;
        readonly Gun _gun;
        readonly Camera _mainCam;
        readonly LayerMask _layer;

        
        public PlayerAttackController(InputHandler inputHandler,
            Camera cam, Gun gun, LayerMask layer, ResourceHandler resourceHandler)
        {
            _inputHandler = inputHandler;
            _mainCam = cam;
            _gun = gun;
            _layer = layer;
            _resourceHandler = resourceHandler;
        }
        
        public void RegisterToInputEvents()
        {
            _inputHandler.Fire += Shoot;
        }

        public void DeRegisterToInputEvents()
        { 
            _inputHandler.Fire -= Shoot;
        }


        void Shoot()
        {
            if (!_resourceHandler.CanShoot()) return;

            RaycastHit rayCast = Raycaster.ShootRay(_mainCam.transform, _layer);
            if (rayCast.collider != null)
            {
                _gun.Fire(rayCast.point);
                if (rayCast.transform.TryGetComponent(out EnemySM enemy))
                {
                    enemy.Die();
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