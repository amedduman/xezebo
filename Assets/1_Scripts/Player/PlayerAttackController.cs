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
        private ResourceHandler _resourceHandler;
        
        readonly Gun _gun;
        readonly Camera _mainCam;
        readonly PlayerInputBroadcaster _inputBroadcaster;
        readonly LayerMask _layer;
        readonly AudioSource _audio;

        
        public PlayerAttackController(PlayerInputBroadcaster inputBroadcaster,
            Camera cam, Gun gun, LayerMask layer, ResourceHandler resourceHandler,
            AudioSource audio)
        {
            _inputBroadcaster = inputBroadcaster;
            _mainCam = cam;
            _gun = gun;
            _layer = layer;
            _resourceHandler = resourceHandler;
            _audio = audio;
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
            if (!_resourceHandler.CanShoot()) return;

            _audio.Play();

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