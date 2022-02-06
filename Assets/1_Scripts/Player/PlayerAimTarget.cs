using UnityEngine;

namespace Player
{
    public class PlayerAimTarget : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] float distanceFromCam = 15;

        void Update()
        {
            transform.position = cam.transform.position + cam.transform.forward * distanceFromCam;
        }
    }

}