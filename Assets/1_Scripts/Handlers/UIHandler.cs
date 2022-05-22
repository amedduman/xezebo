using System;
using TMPro;
using UnityEngine;
using Xezebo.Data;
using Zenject;

namespace Xezebo.Managers
{
    public class UIHandler : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;

        [SerializeField] Canvas _winLevelCanvas;
        [SerializeField] Canvas _failLevelCanvas;

        [SerializeField] TextMeshProUGUI _ammo;
        [SerializeField] TextMeshProUGUI _time;
        [SerializeField] TextMeshProUGUI _health;
        [SerializeField] TextMeshProUGUI _enemyCount;
        
        [SerializeField] LevelTime _levelTime;
        

        private void OnEnable()
        {
            _gameManager.OnWinLevel += HandleWinLevel;
            _gameManager.OnFailLevel += HandleFailLevel;
            _gameManager.OnAmmoUpdated += ChangeAmmoText;
            _gameManager.OnLevelTimeUpdated += ChangeRemainingTimeText;
            _gameManager.OnPlayerHealthUpdated += ChangeHealthUI;
            _gameManager.OnEnemyCountUpdated += ChangeEnemyCountUI;
        }

        private void OnDisable()
        {
            _gameManager.OnWinLevel -= HandleWinLevel;
            _gameManager.OnFailLevel -= HandleFailLevel;
            _gameManager.OnAmmoUpdated -= ChangeAmmoText;
            _gameManager.OnLevelTimeUpdated -= ChangeRemainingTimeText;
            _gameManager.OnPlayerHealthUpdated -= ChangeHealthUI;
            _gameManager.OnEnemyCountUpdated += ChangeEnemyCountUI;
        }

        private void Start()
        {
            _winLevelCanvas.gameObject.SetActive(false);
            _failLevelCanvas.gameObject.SetActive(false);
            
            ChangeRemainingTimeText(_levelTime.LevelTimeData); // since the function will be called after a second from game start, player will see whatever value is on the text mesh component for a second and after that time ui will be correct. we don't want this. so we will update time right after game start.
        }

        void HandleWinLevel()
        {
            _winLevelCanvas.gameObject.SetActive(true);
        }

        void HandleFailLevel()
        {
            _failLevelCanvas.gameObject.SetActive(true);
        }

        void ChangeAmmoText(int ammo)
        {
            _ammo.text = ammo.ToString();
        }

        void ChangeRemainingTimeText(int time)
        {
            _time.text = time.ToString();
        }

        void ChangeHealthUI(int health)
        {
            _health.text = health.ToString();
        }
        
        private void ChangeEnemyCountUI(int count)
        {
            _enemyCount.text = count.ToString();
        }
    }

}