using System;
using UnityEngine;
using Zenject;

namespace Xezebo.Management
{
    public class InputHandler : MonoBehaviour
    {
        [Inject] GameManager _gameManager;

        public event Action Fire;
        public bool Jump;
        public bool Sprint;
        public Vector2 Move;
        public Vector2 Look;

        bool _hasLevelWin;
        bool _hasLevelFail;

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
            if (_hasLevelFail)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                {
                    _gameManager.LevelRestarted();
                }
                ResetInput();
                return;
            }

            if(_hasLevelWin)
            {
                if(UnityEngine.Input.GetKeyDown(KeyCode.Return))
                {
                    _gameManager.NextLevelTriggered();
                }
                ResetInput();
                return;
            }

            //Fire
            if (UnityEngine.Input.GetButtonDown("Fire1"))
            {
                Fire?.Invoke();
            }
            
            //Jump
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                Jump = true;
            }
            if (UnityEngine.Input.GetKeyUp(KeyCode.Space))
            {
                Jump = false;
            }
            
            //Sprint
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
            {
                Sprint = true;
            }
            if (UnityEngine.Input.GetKeyUp(KeyCode.LeftShift))
            {
                Sprint = false;
            }
            

            //Move
            float x = UnityEngine.Input.GetAxis("Horizontal");
            float y = UnityEngine.Input.GetAxis("Vertical");
            Move = new Vector2(x, y);
            
            // Look
            float xLook = UnityEngine.Input.GetAxis("Mouse X");
            float yLook = UnityEngine.Input.GetAxis("Mouse Y");
            if (Mathf.Abs(yLook) < 1)
            {
                yLook = 0;
            }
            Look = new Vector2(xLook, yLook);
        }

        void ResetInput()
        {
            Jump = false;
            Sprint = false;
            Move = Vector2.zero;
            Look = Vector2.zero;
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            SetCursorState(true);
        }

        void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void HandleFailLevel()
        {
            _hasLevelFail = true;
        }

        private void HandleLevelWin()
        {
            _hasLevelWin = true;
        }
    }
}