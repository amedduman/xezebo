using Equipment;
using Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Raycaster))]
    public class PlayerAttackController : MonoBehaviour
    {
        public Gun gun;
        Raycaster raycaster;

        void OnEnable()
        {
            PlayerInputBroadcaster.Instance.Fire().performed += Shoot;
        }

        void OnDisable()
        { 
            PlayerInputBroadcaster.Instance.Fire().performed -= Shoot;
        }

        void Start()
        {
            raycaster = GetComponent<Raycaster>();
        }

        void Shoot(InputAction.CallbackContext Obj)
        {
            if (raycaster.ShootRay().collider != null)
            {
                gun.Fire(raycaster.ShootRay().point);
            }
            // when it looks at the sky bullet goes forward 
            else
            {
                gun.Fire(null);
            }
        }
    }
}