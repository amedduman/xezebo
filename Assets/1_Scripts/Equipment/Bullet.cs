using DG.Tweening;
using UnityEngine;

namespace Xezebo.Equipment
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] int distance = 100;
        // TODO: implement a bullet speed mechanic
        public void ShootAtTarget(Vector3 bulletDestination)
        {
            transform.DOMove(bulletDestination, 0.2f);
            // .OnComplete( () => Destroy(gameObject));
        }

        public void ShootInToTheAir(Transform spawnPoint)
        {
            transform.DOMove(transform.position + spawnPoint.forward * distance, .2f);
                // .OnComplete( () => Destroy(gameObject));
        }

        void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}