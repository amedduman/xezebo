using UnityEngine;
using Xezebo.StateMachine;

namespace Xezebo.Enemy
{
    public class EnemyGetDamageState : IState
    {
        public void Enter()
        {
            Debug.Log("enter get damage state");
        }

        public void Tick()
        {
            // throw new System.NotImplementedException();
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