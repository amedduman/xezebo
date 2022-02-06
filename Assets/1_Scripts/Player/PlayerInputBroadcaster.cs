using UnityEngine;
using UnityEngine.InputSystem;

// TODO: this shouldn't be under this namespace
namespace Player
{
    
public class PlayerInputBroadcaster : MonoBehaviour
{
    // TODO: change the singleton logic. instead writing singleton here. instantiate this class form SingletonTemplate class
    #region singletonInstance

    public static PlayerInputBroadcaster Instance;
    void Awake() => Instance = this;

    #endregion
    
    public bool cursorLocked = true;

    [SerializeField] InputActionReference moveInputActionReference;
    [SerializeField] InputActionReference lookInputActionReference;
    [SerializeField] InputActionReference jumpInputActionReference;
    [SerializeField] InputActionReference sprintInputActionReference;
    [SerializeField] InputActionReference fireInputActionReference;
    

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
        // ben de _canSprint diye bir degiskeni tusd basinca true'ya 
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