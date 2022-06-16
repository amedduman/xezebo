using UnityEngine;
using Xezebo.Equipment;
using Xezebo.Input;
using Xezebo.Managers;
using Zenject;

namespace Xezebo.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        public float MaxHealth = 100;
        [SerializeField] Transform _charMeshParent;
        [Inject] 
        PlayerInputBroadcaster _inputBroadcaster;
        [Inject] 
        ResourceHandler _resourceHandler;
        [Inject(Id = "main")]
        Camera _mainCam;
        [Inject(Id = "activeGun")]
        Gun _gun;

        [SerializeField] LayerMask _layerMask;
        PlayerAttackController attackHandler;
        
        void Awake()
        {
            SetPlayerMesh();

            attackHandler = new PlayerAttackController(_inputBroadcaster, _mainCam, _gun, _layerMask, _resourceHandler);
        }

        void SetPlayerMesh()
        {
            int charMeshIndex = PlayerPrefs.GetInt(MainMenuHandler.PlayerMeshKey);
            for (int i = 0; i < _charMeshParent.childCount; i++)
            {
                _charMeshParent.GetChild(i).gameObject.SetActive(false);
            }
            _charMeshParent.GetChild(charMeshIndex).gameObject.SetActive(true);
        }

        void OnEnable()
        {
            attackHandler.RegisterToInputEvents();
        }

        void OnDisable()
        {
            attackHandler.DeRegisterToInputEvents();
        }
    }
}