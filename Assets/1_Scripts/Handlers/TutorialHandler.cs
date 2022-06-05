using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Xezebo.Data;
using Xezebo.Input;
using Zenject;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] CanvasRenderer _objectivePanel;
    [SerializeField] CanvasRenderer _lowHealthPanel;
    [SerializeField] CanvasRenderer _lowAmmoPanel;
    [SerializeField] CanvasRenderer _lowRemainingTimePanel;
    [Inject] ResourceHandler _resourceHandler; 
    [SerializeField] PlayerMaxAmmo _maxAmmoData;
    [SerializeField] LevelTime _levelTimeData;
    [SerializeField] private PlayerMaxHealth _maxHealthData;
    [SerializeField] PlayerInputBroadcaster _inputBroadCaster;

    CanvasRenderer _activeTutorialPanel;
    bool _hasHealthPanelShowed;
    bool _hasLowAmmoPanelShowed;
    bool _hasLowRemainingTimePanelShowed;
    bool _canPassTutorial;
    
    void Start()
    {
        Time.timeScale = 0;

        _lowHealthPanel.gameObject.SetActive(false);
        _lowAmmoPanel.gameObject.SetActive(false);
        _lowRemainingTimePanel.gameObject.SetActive(false);

        _objectivePanel.gameObject.SetActive(true);
        SetActivePanel(_objectivePanel);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && _canPassTutorial)
        {
            _inputBroadCaster.gameObject.SetActive(true);
            _canPassTutorial = false;
            Time.timeScale = 1;
            _activeTutorialPanel.gameObject.SetActive(false);
        }

        if(_resourceHandler._health < _maxHealthData.PlayerMaxHealthData / 2 && !_hasHealthPanelShowed && _activeTutorialPanel != null)
        {
            Time.timeScale = 0;
            _hasHealthPanelShowed = true;
            _lowHealthPanel.gameObject.SetActive(true);
            SetActivePanel(_lowHealthPanel);
        }
        if(_resourceHandler._ammo < _maxAmmoData.MaxAmmo / 2 && !_hasLowAmmoPanelShowed && _activeTutorialPanel != null)
        {
            Time.timeScale = 0;
            _hasLowAmmoPanelShowed = true;
            _lowAmmoPanel.gameObject.SetActive(true);
            SetActivePanel(_lowAmmoPanel);
        }
        if(_resourceHandler._levelTime < _levelTimeData.LevelTimeData / 2 && !_hasLowRemainingTimePanelShowed && _activeTutorialPanel != null)
        {
            Time.timeScale = 0;
            _hasLowRemainingTimePanelShowed = true;
            _lowRemainingTimePanel.gameObject.SetActive(true);
            SetActivePanel(_lowRemainingTimePanel);
        }
    }

    void SetActivePanel(CanvasRenderer panel)
    {
        _inputBroadCaster.gameObject.SetActive(false);
        _canPassTutorial = false;
        _activeTutorialPanel = panel;
        DOVirtual.DelayedCall(1, () => {
            _canPassTutorial = true;
        });
    }
}
