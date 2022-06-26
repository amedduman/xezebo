using System.Collections.Generic;
using Xezebo.Misc;
using UnityEngine;

namespace Xezebo.Enemy
{
    public class HidingPointsManager : SingletonTemplate<HidingPointsManager>
    {
        [HideInInspector] public List<Transform> HidingPoints = new List<Transform>();
        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                HidingPoints.Add(transform.GetChild(i));
            }
        }

        public Transform GetRandomHidingPoint(ref int? lastRandomPointIndex)
        {
            int rnd = 0;
            if (lastRandomPointIndex != null)
            {
                do
                {
                    
                    rnd = Random.Range(0, HidingPoints.Count);
                    
                } while (rnd == lastRandomPointIndex);
            }
            else
            {
                rnd = Random.Range(0, HidingPoints.Count);
            }
            lastRandomPointIndex = rnd;
            return HidingPoints[rnd];
        }
    }
    
}