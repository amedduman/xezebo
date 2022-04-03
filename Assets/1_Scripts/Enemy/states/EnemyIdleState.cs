using HeurekaGames;
using Xezebo.Player;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyIdleState : IState
    {
        readonly EnemySM enemySm;
        readonly float startToMoveRange;
        readonly PlayerEntity _playerEntity;

        public EnemyIdleState(EnemySM enemySm, float startToMoveRange, PlayerEntity playerEntity)
        {
            this.enemySm = enemySm;
            this.startToMoveRange = startToMoveRange;
            _playerEntity = playerEntity;
        }

        public void Enter()
        {
            // Debug.Log("enter idle state");
            enemySm.enemyStateText.text = "idle";
        }

        public void Tick()
        {
            if (StateHelper.CheckDistance(_playerEntity.transform.position,enemySm.transform.position, startToMoveRange))
            {
                enemySm.ChangeState(enemySm.MoveState);
            }
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
            // Debug.Log("exit idle state");
        }
    }
}

