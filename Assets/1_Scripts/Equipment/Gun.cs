using UnityEngine;

namespace Equipment
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] Transform bulletSpawnPoint;
        [SerializeField] Bullet bulletPrefab;

        public void Fire(Vector3? bulletDestination = null)
        {
            Bullet bullet = SpawnBullet();
            if (bulletDestination != null)
            {
                bullet.ShootAtTarget((Vector3) bulletDestination);
            }
            else
            {
                bullet.ShootInToTheAir(bulletSpawnPoint);
            }
        }

        Bullet SpawnBullet()
        {
            // TODO: object pooling
            return Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        }
    }
}
