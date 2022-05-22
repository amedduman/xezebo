using System.Collections.Generic;
using UnityEngine;
using Xezebo.Enemy;
using Zenject;

namespace Xezebo.Managers
{
    public class EnemyHandler : MonoBehaviour
    {
        [Inject] GameManager _gameManager;
        
        List<EnemySM> ActiveEnemies { get;} = new List<EnemySM>();

        public void RegisterEnemy(EnemySM enemy)
        {
            ActiveEnemies.Add(enemy);
            _gameManager.EnemyCountUpdated(ActiveEnemies.Count);
        }

        public void DeregisterEnemy(EnemySM enemy)
        {
            ActiveEnemies.Remove(enemy);
            _gameManager.EnemyCountUpdated(ActiveEnemies.Count);
        }
    }
    
    
}