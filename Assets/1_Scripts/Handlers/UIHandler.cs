using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using Xezebo.Data;
using Xezebo.DI;
using Zenject;

namespace Xezebo.Management
{
    public class UIHandler : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        [Inject(Id = nameof(BindingIdentifiers.win_level_canvas))] Canvas _winLevelCanvas;
        [Inject(Id = nameof(BindingIdentifiers.fail_level_canvas))] Canvas _failLevelCanvas;
        [Inject(Id = nameof(BindingIdentifiers.ammo_text))] TextMeshProUGUI _ammo;
        [Inject(Id = nameof(BindingIdentifiers.time_text))] TextMeshProUGUI _time;
        [Inject(Id = nameof(BindingIdentifiers.health_text))] TextMeshProUGUI _health;
        [Inject(Id = nameof(BindingIdentifiers.enemy_count_text))] TextMeshProUGUI _enemyCount;
        
        GameValues _gameValues;

        private void OnEnable()
        {
            _gameManager.OnWinLevel += HandleWinLevel;
            _gameManager.OnFailLevel += HandleFailLevel;
            _gameManager.OnAmmoUpdated += ChangeAmmoText;
            _gameManager.OnLevelTimeUpdated += ChangeTimeText;
            _gameManager.OnPlayerHealthUpdated += ChangeHealthUI;
            _gameManager.OnEnemyCountUpdated += ChangeEnemyCountUI;
        }

        private void OnDisable()
        {
            _gameManager.OnWinLevel -= HandleWinLevel;
            _gameManager.OnFailLevel -= HandleFailLevel;
            _gameManager.OnAmmoUpdated -= ChangeAmmoText;
            _gameManager.OnLevelTimeUpdated -= ChangeTimeText;
            _gameManager.OnPlayerHealthUpdated -= ChangeHealthUI;
            _gameManager.OnEnemyCountUpdated += ChangeEnemyCountUI;
        }

        private void Start()
        {
            _winLevelCanvas.gameObject.SetActive(false);
            _failLevelCanvas.gameObject.SetActive(false);

            _gameValues = Resources.Load<GameValues>("GameValues");
            // If we don't set the values right away they will be wrong until first call of these functions
            ChangeTimeText(_gameValues.LevelTimeData);
            ChangeAmmoText(_gameValues.MaxAmmo);
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

        void ChangeTimeText(int time)
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