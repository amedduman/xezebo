using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyDeathState : IState
    {
        readonly EnemyAnimationController animationController;
        readonly Collider hitBox;
        readonly EnemyMover mover;
        readonly Canvas hpCanvas;

        public EnemyDeathState(EnemyAnimationController animationController, Collider hitBox, EnemyMover mover, Canvas hpCanvas)
        {
            this.animationController = animationController;
            this.hitBox = hitBox;
            this.mover = mover;
            this.hpCanvas = hpCanvas;
        }
        
        public void Enter()
        {
            mover.Die();
            hitBox.enabled = false;
            animationController.TurnRagdoll();
            hpCanvas.enabled = false;
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