using Player;
using UnityEngine;
using StateMachine;

namespace Enemy
{
    public class EnemyIdleState : IState
    {
        readonly EnemySM enemySm;
        readonly float startToMoveRange;

        public EnemyIdleState(EnemySM enemySm, float startToMoveRange)
        {
            this.enemySm = enemySm;
            this.startToMoveRange = startToMoveRange;
        }

        public void Enter()
        {
            // Debug.Log("enter idle state");
            enemySm.enemyStateText.text = "idle";
        }

        public void Tick()
        {
            if (StateHelper.CheckDistance(PlayerRef.Instance.transform.position,enemySm.transform.position, startToMoveRange))
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

