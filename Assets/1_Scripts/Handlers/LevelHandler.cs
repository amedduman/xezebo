using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Xezebo.Managers
{
    public class LevelHandler : MonoBehaviour
    {
        [Inject] GameManager _gameManager;

        private bool _hasLevelEnd;
        
        private void OnEnable()
        {
            _gameManager.OnWinLevel += HandleLevelWin;
            _gameManager.OnFailLevel += HandleFailLevel;
        }

        private void OnDisable()
        {
            _gameManager.OnWinLevel -= HandleLevelWin;
            _gameManager.OnFailLevel -= HandleFailLevel;
        }

        private void Update()
        {
            if (_hasLevelEnd)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                {
                    RestartLevel();
                }
            }
        }

        private void HandleFailLevel()
        {
            _hasLevelEnd = true;
        }

        private void HandleLevelWin()
        {
            _hasLevelEnd = true;
        }

        void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}