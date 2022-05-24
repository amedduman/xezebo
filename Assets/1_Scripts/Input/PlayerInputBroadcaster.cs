using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Xezebo.Management;
using Zenject;

namespace Xezebo.Input
{
    
public class PlayerInputBroadcaster : MonoBehaviour
{
    [Inject] private GameManager _gameManager;
    
    public bool cursorLocked = true;

    [SerializeField] InputActionReference moveInputActionReference;
    [SerializeField] InputActionReference lookInputActionReference;
    [SerializeField] InputActionReference jumpInputActionReference;
    [SerializeField] InputActionReference sprintInputActionReference;
    [SerializeField] InputActionReference fireInputActionReference;

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
    
    private void HandleFailLevel()
    {
        Destroy(gameObject);
    }

    private void HandleWinLevel()
    {
        Destroy(gameObject);
    }

    public Vector2 Move()
    {
        return moveInputActionReference.action.ReadValue<Vector2>();
    }

    public Vector2 Look()
    {
        return lookInputActionReference.action.ReadValue<Vector2>();
    }
    
    public bool Jump()
    {
        return jumpInputActionReference.action.triggered;
    }

    public InputAction Fire()
    {
        return fireInputActionReference.action; 
        // return fireInputActionReference.action.triggered;
    }

    bool _canSprint = false;
    public bool Sprint()
    {
        // cok guzel bi cozum degil ama simdilik kalsin
        // calisma mantigi PlayerInputActions icinde sprint action'u passThrough olarak ayarlandi
        // boylece her deger degistiginde (yani butona basilip cekildiginde) triggered cagriliyor
        // ben de _canSprint diye bir degiskeni tusa basinca true'ya 
        // tusdan parmagi cekince false'a esitliyorum
        // boylece tusa basili tutulunca sprint edilmis oluyor
        
        if (!_canSprint)
        {
            _canSprint = sprintInputActionReference.action.triggered;
        }
        else
        {
            _canSprint = !sprintInputActionReference.action.triggered;
        }

        return _canSprint;
    }
    
    void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}   
}