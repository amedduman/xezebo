using System.Collections.Generic;
using Xezebo.Misc;
using UnityEngine;

namespace Xezebo.Enemy
{
    public class HidingPointsManager : SingletonTemplate<HidingPointsManager>
    {
        public List<Transform> HidingPoints = new List<Transform>();

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