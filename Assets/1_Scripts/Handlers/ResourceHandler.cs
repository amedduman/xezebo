using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Xezebo.Data;
using Xezebo.Management;
using Zenject;

public class ResourceHandler : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    GameValues _gameValues;
    
    int _ammo;
    int _levelTime;
    int _health;

    Coroutine _decreaseHealthCoroutine;
    Coroutine _increaseHealthCoroutine;
    Coroutine _increaseAmmoCoroutine;

    bool _hasLevelEnd;

    private void OnEnable()
    {
        _gameManager.OnWinLevel += HandleWinLevel;
        _gameManager.OnFailLevel += HandleFailLevel;
    }

    private void OnDisable()
    {
        _gameManager.OnWinLevel -= HandleWinLevel;
        _gameManager.OnFailLevel -= HandleFailLevel;
    }

    void Start()
    {
        _gameValues = Resources.Load<GameValues>("GameValues");
        if (_gameValues == null)
        {
            Debug.Log("null game values");
        }
        _ammo = _gameValues.MaxAmmo;
        _levelTime = _gameValues.LevelTimeData;
        _health = _gameValues.PlayerMaxHealthData;

        StartCoroutine(DecreaseLevelTime());
        _decreaseHealthCoroutine = StartCoroutine(DecreaseHealth());
    }
    
    private void HandleFailLevel()
    {
        _hasLevelEnd = true;
        StopAllCoroutines();
    }

    private void HandleWinLevel()
    {
        _hasLevelEnd = true;
        StopAllCoroutines();
    }
    
    public void StartRefillResources()
    {
        if (_hasLevelEnd) return;
        
        StopCoroutine(_decreaseHealthCoroutine);
        _increaseHealthCoroutine = StartCoroutine(IncreaseHealth());

        _increaseAmmoCoroutine = StartCoroutine(IncreaseAmmo());
    }

    public void StopRefillResources()
    {
        if (_hasLevelEnd) return;
        
        StopCoroutine(_increaseHealthCoroutine);
        _decreaseHealthCoroutine = StartCoroutine(DecreaseHealth());
        
        StopCoroutine(_increaseAmmoCoroutine);
    }

    IEnumerator DecreaseLevelTime()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            _levelTime--;
            ClampLevelTime();
            _gameManager.LevelTimeUpdated(_levelTime);
        }
    }

    IEnumerator DecreaseHealth()
    { 
        while (true)
        {
            yield return new WaitForSecondsRealtime(.2f); // no magic numbers!!!
            _health -= 2; // no magic numbers!!!
            ClampHealth();
            _gameManager.PlayerHealthUpdated(_health);
        }
    }

    IEnumerator IncreaseHealth()
    { 
        while (true)
        {
            _health += 2; // no magic numbers!!!
            yield return new WaitForSecondsRealtime(.2f);
            ClampHealth();
            _gameManager.PlayerHealthUpdated(_health);
        }
    }

    IEnumerator IncreaseAmmo()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1); // no magic numbers!!!
            _ammo++;
            ClampAmmo();
            _gameManager.AmmoUpdated(_ammo);
        }
    }

    void ClampLevelTime()
    {
        _levelTime = Mathf.Clamp(_levelTime, 0, _gameValues.LevelTimeData);
    }

    void ClampHealth()
    {
        _health = Mathf.Clamp(_health, 0, _gameValues.PlayerMaxHealthData);
    }

    void ClampAmmo()
    {
        _ammo = Mathf.Clamp(_ammo, 0, _gameValues.MaxAmmo);
    }

    public bool CanShoot()
    {
        if (_ammo <= 0) return false;

        UpdateAmmo();

        return true;
    }

    void UpdateAmmo()
    {
        _ammo--;
        _ammo = Mathf.Clamp(_ammo,0, _gameValues.MaxAmmo);
        _gameManager.AmmoUpdated(_ammo);
    }
}
