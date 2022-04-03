using UnityEngine;
using Xezebo.Equipment;
using Xezebo.Input;
using Zenject;
using Xezebo.Player;

public class GameInstaller : MonoInstaller
{
    [SerializeField] PlayerEntity _playerEntity;
    [SerializeField] Camera _mainCam;
    [SerializeField] Gun _activeGun;
    [SerializeField] PlayerInputBroadcaster _playerInputBroadcaster;


    public override void InstallBindings()
    {
        Container.Bind<PlayerInputBroadcaster>().FromInstance(_playerInputBroadcaster).AsSingle().NonLazy();
        Container.Bind<PlayerEntity>().FromInstance(_playerEntity).AsSingle().NonLazy();
        Container.Bind<Camera>().WithId("main").FromInstance(_mainCam).AsSingle().NonLazy(); // kimler inject ediyo kamerayi nasil gorebilirim? infiliable code'ta anlatiyodu sanki ilk videoda
        Container.Bind<Gun>().WithId("activeGun").FromInstance(_activeGun).AsSingle().NonLazy();
    }
}