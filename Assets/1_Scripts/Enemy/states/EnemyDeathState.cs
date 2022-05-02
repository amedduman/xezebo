using UnityEngine;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyDeathState : IState
    {
        readonly EnemyAnimationController animationController;
        readonly Collider hitBox;
        readonly EnemyMover mover;

        public EnemyDeathState(EnemyAnimationController animationController, Collider hitBox, EnemyMover mover)
        {
            this.animationController = animationController;
            this.hitBox = hitBox;
            this.mover = mover;
        }
        
        public void Enter()
        {
            mover.Die();
            hitBox.enabled = false;
            animationController.TurnRagdoll();
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