using System;
using Player;
using UnityEngine;
using StateMachine;
using TMPro;

namespace Enemy
{
    public class EnemySM : StateMachineMB
    {
        // states
        public IState IdleState { get; private set; }
        public IState MoveState { get; private set; }

        // debugging
        public TextMeshPro enemyStateText;
        
        // required
        EnemyMover enemyMover;
        EnemyAnimationController enemyAnimationController;
        [SerializeField] float startToMoveRange = 5;
        

        void Awake()
        {
            enemyMover = GetComponent<EnemyMover>();
            enemyAnimationController = GetComponent<EnemyAnimationController>();
            
            IdleState = new EnemyIdleState(this, startToMoveRange);
            MoveState = new EnemyMoveState(this, enemyMover);
        }

        void Start()
        {
            ChangeState(IdleState);
        }

        public override void OnStateChange(IState State)
        {
            enemyAnimationController.StateChange(State);
        }

        void OnDrawGizmos()
        {
            // when in idle state this will be the range. if player pass this the enemy will give reaction accordingly.
            Gizmos.DrawWireSphere(transform.position, startToMoveRange);
        }
    }
}

