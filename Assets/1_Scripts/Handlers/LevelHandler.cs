using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Xezebo.Managers
{
    public class LevelHandler : MonoBehaviour
    {
        [Inject] GameManager _gameManager;

        bool _hasLevelFail;
        bool _hasLevelWin;
        
        void OnEnable()
        {
            _gameManager.OnWinLevel += HandleLevelWin;
            _gameManager.OnFailLevel += HandleFailLevel;
        }

        void OnDisable()
        {
            _gameManager.OnWinLevel -= HandleLevelWin;
            _gameManager.OnFailLevel -= HandleFailLevel;
        }

        void Update()
        {
            if (_hasLevelFail)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                {
                    RestartLevel();
                }
            }

            else if(_hasLevelWin)
            {
                if(UnityEngine.Input.GetKeyDown(KeyCode.Return))
                {
                    LoadNextLevel();
                }
            }

            #if UNITY_EDITOR
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                {
                    RestartLevel();
                }
            #endif
        }

        void HandleFailLevel()
        {
            _hasLevelFail = true;
        }

        void HandleLevelWin()
        {
            _hasLevelWin = true;
        }

        void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void LoadNextLevel()
        {
            string activeSceneBuildIndex = SceneManager.GetActiveScene().name;
            if(activeSceneBuildIndex == "Tutorial")
            {
                SceneManager.LoadScene("Level_1");
            }
            else if(activeSceneBuildIndex == "Level_1")
            {
                SceneManager.LoadScene("Level_2");
            }
            else if(activeSceneBuildIndex == "Level_2")
            {
                SceneManager.LoadScene("Level_1");
            }
        }
    }
}