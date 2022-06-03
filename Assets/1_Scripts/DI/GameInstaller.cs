using TMPro;
using UnityEngine;
using Xezebo.Equipment;
using Xezebo.Management;
using Zenject;
using Xezebo.Player;
using Xezebo.Enemy;

public class GameInstaller : MonoInstaller
{
    [SerializeField] GameManager _gameManager;
    
    [SerializeField] UIHandler _uiHandler;
    [SerializeField] EnemyHandler _enemyHandler;
    [SerializeField] ResourceHandler _resourceHandler;
    [SerializeField] HidingPointsHandler _hidingPointsHandler;
    
    [SerializeField] PlayerEntity _playerEntity;
    [SerializeField] Camera _mainCam;
    [SerializeField] Gun _activeGun;


    public override void InstallBindings()
    {
        // manager
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();

        // handlers
        Container.Bind<UIHandler>().FromInstance(_uiHandler).AsSingle().NonLazy();
        Container.Bind<ResourceHandler>().FromInstance(_resourceHandler).AsSingle().NonLazy();
        Container.Bind<EnemyHandler>().FromInstance(_enemyHandler).AsSingle().NonLazy();
        Container.Bind<HidingPointsHandler>().FromInstance(_hidingPointsHandler).AsSingle().NonLazy();

        // Entities
        Container.Bind<PlayerEntity>().FromInstance(_playerEntity).AsSingle().NonLazy();
        Container.Bind<Camera>().WithId("main").FromInstance(_mainCam).AsSingle().NonLazy(); // kimler inject ediyo kamerayi nasil gorebilirim? infiliable code'ta anlatiyodu sanki ilk videoda
        Container.Bind<Gun>().WithId("activeGun").FromInstance(_activeGun).AsSingle().NonLazy();
    }
}