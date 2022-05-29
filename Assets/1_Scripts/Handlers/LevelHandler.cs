using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Xezebo.Management
{
    public class LevelHandler : MonoBehaviour
    {
        [Inject] GameManager _gameManager;

        private void OnEnable()
        {
            _gameManager.OnLevelRestart += RestartLevel;
        }

        private void OnDisable()
        {
            _gameManager.OnLevelRestart -= RestartLevel;
        }

        void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}