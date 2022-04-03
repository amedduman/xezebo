using Xezebo.Enemy;
using UnityEngine;

namespace Construct
{
    public class Construct : MonoBehaviour
    {
        [SerializeField] Transform hidingPointsParent;

        void Start()
        {
            for (int i = 0; i < hidingPointsParent.childCount; i++)
            {
                HidingPointsManager.Instance.HidingPoints.Add(hidingPointsParent.GetChild(i));
            }
        }
    }
}
