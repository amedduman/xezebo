using UnityEngine;
using Xezebo.Management;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyDeathState : IState
    {
        private readonly EnemySM enemySm;
        readonly EnemyAnimationController animationController;
        readonly Collider hitBox;
        readonly EnemyMover mover;
        readonly GameObject bloodParticle;
        readonly EnemyHandler _enemyHandler;

        public EnemyDeathState(EnemySM enemySm, EnemyAnimationController animationController, Collider hitBox,
            EnemyMover mover, GameObject bloodParticle, EnemyHandler enemyHandler)
        {
            this.enemySm = enemySm;
            this.animationController = animationController;
            this.hitBox = hitBox;
            this.mover = mover;
            this.bloodParticle = bloodParticle;
            _enemyHandler = enemyHandler;
        }
        
        public void Enter()
        {
            mover.Die();
            hitBox.enabled = false;
            animationController.TurnRagdoll();
            bloodParticle.SetActive(true);
            _enemyHandler.DeregisterEnemy(enemySm);
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }
    }
}