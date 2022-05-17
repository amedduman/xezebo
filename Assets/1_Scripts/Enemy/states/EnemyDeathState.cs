using UnityEngine;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyDeathState : IState
    {
        readonly EnemyAnimationController animationController;
        readonly Collider hitBox;
        readonly EnemyMover mover;
        private readonly GameObject bloodParticle;

        public EnemyDeathState(EnemyAnimationController animationController, Collider hitBox,
            EnemyMover mover, GameObject bloodParticle)
        {
            this.animationController = animationController;
            this.hitBox = hitBox;
            this.mover = mover;
            this.bloodParticle = bloodParticle;
        }
        
        public void Enter()
        {
            mover.Die();
            hitBox.enabled = false;
            animationController.TurnRagdoll();
            bloodParticle.SetActive(true);
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