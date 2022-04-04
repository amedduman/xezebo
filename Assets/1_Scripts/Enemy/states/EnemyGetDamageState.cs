using UnityEngine;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyGetDamageState : IState
    {
        readonly EnemySM enemySm;

        public EnemyGetDamageState(EnemySM enemySm)
        {
            this.enemySm = enemySm;
        }
        
        public void Enter()
        {
            enemySm.Hp -= enemySm.DamagePerShot;
        }

        public void Tick()
        {
            if (enemySm.Hp > 0)
            {
                enemySm.ChangeState(enemySm.MoveState);
            }
            else
            {
                enemySm.ChangeState(enemySm.DeathSate);
            }
        }

        public void FixedTick()
        {
            // throw new System.NotImplementedException();
        }

        public void Exit()
        {
            Debug.Log("exit get damage state");
        }
    }
}