using UnityEngine;
using Xezebo.Player;
using Zenject;

namespace Xezebo.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject] PlayerEntity _playerEntity;
        [Inject] UIHandler _uiHandler;
        
        int _maxAmmo;
        int _ammo;

        void Start()
        {
            _maxAmmo = _playerEntity.MaxAmmo;
            _ammo = _maxAmmo;
        }

        public bool CanShoot()
        {
            if (_ammo <= 0) return false;

            _ammo--;
            _ammo = Mathf.Clamp(_ammo,0, _maxAmmo);
            _uiHandler.ChangeAmmoText(_ammo);
            
            return true;
        }
    }
}
