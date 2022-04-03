using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Xezebo.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        Transform[] hidingPoints;
        int? currentHidingPointIndex = null;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        
        public void Move(Action InvokeWhenMoveComplete)
        {
            var dst = GetRandomDestination();
            SetDestination(dst);
            StartCoroutine(CheckMovementSituation(InvokeWhenMoveComplete, dst));
        }

        void SetDestination(Vector3 destination)
        {
            agent.destination = destination;
        }

        // TODO: currently this function assumes the enemy will be able to reach destination every single time.
        // but there can be situation which enemy cannot reach destination and stop
        // the function needs to check enemy speed and if it is zero (if enemy stopped) then 
        // also invoke the movement complete action 
        IEnumerator CheckMovementSituation(Action InvokeWhenMoveComplete, Vector3 destination)
        {
            float stoppingDist = 0;
            float assumedStoppingDistance = 5; // this will use for stopping distance. when stopping distance of agent is set to 0
            if (agent.stoppingDistance <= 0)
            {
                Debug.LogWarning($"stopping distance for {gameObject.name} is equal to zero. In this case " +
                                 $"program cannot detect if {gameObject.name} is arrived its destination." +
                                 $"to make it work properly, program assume stopping distance as {assumedStoppingDistance}" +
                                 $"if you change stopping distance of {gameObject.name} to something bigger than 0." +
                                 $"It will fix the issue.", gameObject);
                stoppingDist = assumedStoppingDistance;
            }
            else
            {
                stoppingDist = agent.stoppingDistance;
            }
            
            while (true)
            {
                if (StateHelper.CheckDistance(destination,transform.position, stoppingDist))
                {
                    InvokeWhenMoveComplete.Invoke();
                    yield break;
                }
                yield return null; // do we need check every frame?
            }
        }

        Vector3 GetRandomDestination()
        {
            return HidingPointsManager.Instance.GetRandomHidingPoint(ref currentHidingPointIndex).position;
        }
    }
}
