using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Xezebo.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action<int> OnAmmoUpdated;
        public event Action<int> OnLevelTimeUpdated;
        public event Action<int> OnPlayerHealthUpdated;

        public void AmmoUpdated(int ammo)
        {
            OnAmmoUpdated?.Invoke(ammo);
        }

        public void LevelTimeUpdated(int time)
        {
            OnLevelTimeUpdated?.Invoke(time);
            CheckFail(time);
        }

        public void PlayerHealthUpdated(int health)
        {
            OnPlayerHealthUpdated?.Invoke(health);
            CheckFail(health);
        }

        [SerializeField] private GameObject tempGameOverUI;
        private void CheckFail(int time)
        {
            if (time <= 0)
            {
                Time.timeScale = 0;
                tempGameOverUI.SetActive(true);
            }
        }

        private void Start()
        {
            Time.timeScale = 1;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                RestartTheGame();
            }
        }

        void RestartTheGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
