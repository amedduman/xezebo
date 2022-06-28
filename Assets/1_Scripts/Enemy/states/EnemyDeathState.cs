using UnityEngine;
using Xezebo.Managers;
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
        readonly SpriteRenderer _minimapSymbol;
        readonly AudioSource _terminatedSound;
        

        public EnemyDeathState(EnemySM enemySm, EnemyAnimationController animationController, Collider hitBox,
            EnemyMover mover, GameObject bloodParticle, EnemyHandler enemyHandler, 
            SpriteRenderer minimapSymbol, AudioSource terminatedSound)
        {
            this.enemySm = enemySm;
            this.animationController = animationController;
            this.hitBox = hitBox;
            this.mover = mover;
            this.bloodParticle = bloodParticle;
            _enemyHandler = enemyHandler;
            _minimapSymbol = minimapSymbol;
            _terminatedSound = terminatedSound;
        }
        
        public void Enter()
        {
            mover.Die();
            hitBox.enabled = false;
            animationController.TurnRagdoll();
            bloodParticle.SetActive(true);
            _enemyHandler.DeregisterEnemy(enemySm);
            _minimapSymbol.gameObject.SetActive(false);
            _terminatedSound.Play();
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