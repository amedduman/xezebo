using Xezebo.StateMachine;
using UnityEngine;

namespace Xezebo.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] Animator animator;

        public void StateChange(IState State)
        {
            if (State is EnemyIdleState)
            {
                animator.Play("Idle");
            }
            
            else if (State is EnemyMoveState)
            {
                animator.Play("Running");
            }
        }
    }
}