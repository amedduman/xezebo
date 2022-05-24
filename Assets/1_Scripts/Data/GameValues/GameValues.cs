using UnityEngine;

namespace Xezebo.Data
{
    [CreateAssetMenu(fileName = "GameValues", menuName = "SO/GameValues", order = 0)]
    public class GameValues : ScriptableObject
    {
        public int LevelTimeData
        {
            get
            {
                return _levelTime;
            }
            private set
            {
                
            }
        }

        [SerializeField] int _levelTime = 20;
        
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