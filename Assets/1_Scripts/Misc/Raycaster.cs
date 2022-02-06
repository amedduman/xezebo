using UnityEngine;

namespace Misc
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] Transform origin;
    
        public RaycastHit ShootRay()
        {
            Ray ray = new Ray(origin.position, origin.forward);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo);

            if (hasHit)
            {
                // hitPos = hitInfo.point;
                return hitInfo;
            }
            
            return hitInfo;
        }
    }
}
