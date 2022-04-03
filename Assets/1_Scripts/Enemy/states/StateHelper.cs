using Xezebo.Player;
using UnityEngine;

namespace Xezebo.Enemy
{
    public static class StateHelper
    {
        // TODO: try to make it method extension for transform?
        public static bool CheckDistance(Vector3 pos1, Vector3 pos2, float range)
        {
            return Vector3.Distance(pos1, pos2) < range;
        }
    }
}