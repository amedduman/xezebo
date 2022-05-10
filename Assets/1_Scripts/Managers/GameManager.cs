using UnityEngine;
using Xezebo.Data;
using Xezebo.Player;
using Zenject;

namespace Xezebo.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject] UIHandler _uiHandler;

        [SerializeField] PlayerMaxAmmo _maxAmmoData;
        
        
        int _maxAmmo;
        int _ammo;

        void Start()
        {
            _maxAmmo = _maxAmmoData.MaxAmmo;
            _ammo = _maxAmmo;
        }

        public bool CanShoot()
        {
            if (_ammo <= 0) return false;

            UpdateAmmo();

            return true;
        }

        void UpdateAmmo()
        {
            _ammo--;
            _ammo = Mathf.Clamp(_ammo,0, _maxAmmo);
            _uiHandler.ChangeAmmoText(_ammo);
        }
    }
}
