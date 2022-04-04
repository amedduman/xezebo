using UnityEngine;
using UnityEngine.UI;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyGetDamageState : IState
    {
        readonly EnemySM enemySm;
        readonly Image hpImage;

        public EnemyGetDamageState(EnemySM enemySm, Image hpImage)
        {
            this.enemySm = enemySm;
            this.hpImage = hpImage;
        }
        
        public void Enter()
        {
            enemySm.Hp -= enemySm.DamagePerShot;
            enemySm.Hp = Mathf.Clamp(enemySm.Hp, 0, enemySm.HpMax);
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

            hpImage.fillAmount = (float)enemySm.Hp / enemySm.HpMax;
        }

        public void FixedTick()
        {
            // throw new System.NotImplementedException();
        }

        public void Exit()
        {
        }
    }
}