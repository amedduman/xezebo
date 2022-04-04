using UnityEngine;

namespace Xezebo.Misc
{
    public static class Raycaster
    {
        public static RaycastHit ShootRay(Transform origin, LayerMask layer)
        {
            Ray ray = new Ray(origin.position, origin.forward);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layer);

            if (hasHit)
            {
                // hitPos = hitInfo.point;
                return hitInfo;
            }
            
            return hitInfo;
        }
    }
}
