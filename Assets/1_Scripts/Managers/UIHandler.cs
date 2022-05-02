using TMPro;
using UnityEngine;

namespace Xezebo.Managers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _ammo;
        
        public void ChangeAmmoText(int ammo)
        {
            _ammo.text = ammo.ToString();
        }
    }

}