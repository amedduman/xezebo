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
        
        [SerializeField] TextMeshProUGUI _ammo;
        [SerializeField] TextMeshProUGUI _time;
        [SerializeField] TextMeshProUGUI _health;
        
        [SerializeField] LevelTime _levelTime;
        

        private void OnEnable()
        {
            _gameManager.OnAmmoUpdated += ChangeAmmoText;
            _gameManager.OnLevelTimeUpdated += ChangeRemainingTimeText;
            _gameManager.OnPlayerHealthUpdated += ChangeHealthUI;
        }

        private void OnDisable()
        {
            _gameManager.OnAmmoUpdated -= ChangeAmmoText;
            _gameManager.OnLevelTimeUpdated -= ChangeRemainingTimeText;
            _gameManager.OnPlayerHealthUpdated -= ChangeHealthUI;
        }

        private void Start()
        {
            ChangeRemainingTimeText(_levelTime.LevelTimeData); // since the function will be called after a second from game start, player will see whatever value is on the text mesh component for a second and after that time ui will be correct. we don't want this. so we will update time right after game start.
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
    }

}