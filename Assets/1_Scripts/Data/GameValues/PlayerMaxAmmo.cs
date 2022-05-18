using UnityEngine;

namespace Xezebo.Data
{
    [CreateAssetMenu(fileName = "PlayerMaxAmmo", menuName = "SO/GameValues", order = 0)]
    public class PlayerMaxAmmo : ScriptableObject
    {
        public int MaxAmmo {
            get
            {
                return _playerMaxAmmo;
            }
            private set
            {
                
            }
        }

        [SerializeField] int _playerMaxAmmo = 10;
    }
}