using UnityEngine;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyMoveState : IState
    {
        #region state_code

        readonly EnemySM enemySm;
        readonly EnemyMover enemyMover;

        public EnemyMoveState(EnemySM enemySm, EnemyMover enemyMover)
        {
            this.enemySm = enemySm;
            this.enemyMover = enemyMover;
        }
    
        public void Enter()
        {
            // Debug.Log("enter move state");
            enemySm.enemyStateText.text = "move";
            enemyMover.Move(MovementResult);
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
            // Debug.Log("exit move state");
        }

        #endregion

        void MovementResult()
        {
            enemySm.ChangeState(enemySm.IdleState);
        }
    }

}
