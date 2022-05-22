using Xezebo.Player;
using UnityEngine;
using Xezebo.StateMachine;
using TMPro;
using Xezebo.Managers;
using Zenject;

namespace Xezebo.Enemy
{
    public class EnemySM : StateMachineMB
    {
        [Inject] private EnemyHandler _enemyHandler;
        
        // states
        public IState IdleState { get; private set; }
        public IState MoveState { get; private set; }
        public IState GetDamageState { get; private set; }
        public IState DeathSate { get; private set; }

        // debugging
        public TextMeshPro enemyStateText;

        // required
        EnemyMover enemyMover;
        EnemyAnimationController enemyAnimationController;
        [SerializeField] float startToMoveRange = 5;
        [SerializeField] private GameObject _bloodParticle;

        

        PlayerEntity _playerEntity;

        [Inject]
        void Construct(PlayerEntity playerEntity)
        {
            _playerEntity = playerEntity;
        }
        
        void Awake()
        {
            enemyMover = GetComponent<EnemyMover>();
            enemyAnimationController = GetComponent<EnemyAnimationController>();
            Collider hitBox = GetComponent<Collider>();
            
            IdleState = new EnemyIdleState(this, startToMoveRange, _playerEntity);
            MoveState = new EnemyMoveState(this, enemyMover);
            DeathSate = new EnemyDeathState(this, enemyAnimationController, hitBox, enemyMover, _bloodParticle, _enemyHandler);
        }

        void Start()
        {
            ChangeState(IdleState);
            _enemyHandler.RegisterEnemy(this);
        }

        public void Die()
        {
            ChangeState(DeathSate);
        }

        public override void OnStateChange(IState State)
        {
            enemyAnimationController.StateChange(State);
        }

        void OnDrawGizmosSelected()
        {
            // when in idle state this will be the range. if player pass this the enemy will give reaction accordingly.
            Gizmos.DrawWireSphere(transform.position, startToMoveRange);
        }
    }
}

