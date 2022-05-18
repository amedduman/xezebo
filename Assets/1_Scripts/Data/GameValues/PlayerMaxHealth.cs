using UnityEngine;

namespace Xezebo.Data
{
    [CreateAssetMenu(fileName = "PlayerMaxHealth", menuName = "SO/PlayerMaxHealth", order = 0)]
    public class PlayerMaxHealth : ScriptableObject
    {
        public int PlayerMaxHealthData
        {
            get
            {
                return _playerMaxHealth;
            }
            set
            {
                
            }
        }

        [SerializeField] private int _playerMaxHealth = 100;

    }
}