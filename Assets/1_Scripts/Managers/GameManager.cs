using System;
using UnityEngine;

namespace Xezebo.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnWinLevel;
        public event Action OnFailLevel;
        public event Action<int> OnAmmoUpdated;
        public event Action<int> OnLevelTimeUpdated;
        public event Action<int> OnPlayerHealthUpdated;
        public event Action<int> OnEnemyCountUpdated;

        private int _remainingEnemyCount;

        void WinLevel()
        {
            OnWinLevel?.Invoke();
        }

        void FailLevel()
        {
            OnFailLevel?.Invoke();
        }

        bool CheckWinState()
        {
            return _remainingEnemyCount == 0;
        }

        public void AmmoUpdated(int ammo)
        {
            OnAmmoUpdated?.Invoke(ammo);
        }

        public void LevelTimeUpdated(int time)
        {
            if (time <= 0)
            {
                if (CheckWinState())
                {
                    WinLevel();
                }
                else
                {
                    FailLevel();
                }
            }
            OnLevelTimeUpdated?.Invoke(time);
        }

        public void PlayerHealthUpdated(int health)
        {
            if (health <= 0)
            {
                FailLevel();
            }
            OnPlayerHealthUpdated?.Invoke(health);
        }

        public void EnemyCountUpdated(int count)
        {
            if (count < 0)
            {
                Debug.LogError("there is an error with enemy count logic.");
                count = 0;
            }

            _remainingEnemyCount = count;
            OnEnemyCountUpdated?.Invoke(count);
        }
    }
}
