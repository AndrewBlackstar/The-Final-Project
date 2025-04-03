using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public BulletPool3D bulletPool;
    public Transform shootPoint;

    void Update()
    {
        if (shootPoint != null)
        {
            shootPoint.position = transform.position; // O usa el bone de la animación
            shootPoint.rotation = transform.rotation;
        }
    }

    public void FireBullet()
    {
        if (bulletPool == null)
        {
            Debug.LogError($"❌ {gameObject.name}: BulletPool3D no está asignado.");
            return;
        }

        if (shootPoint == null)
        {
            Debug.LogError($"❌ {gameObject.name}: ShootPoint no está asignado.");
            return;
        }

        GameObject bullet = bulletPool.GetBullet();
        if (bullet == null)
        {
            Debug.LogError("❌ No hay balas disponibles en el BulletPool.");
            return;
        }

        bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
        bullet.SetActive(true);

        Bullet3D bulletScript = bullet.GetComponent<Bullet3D>();
        if (bulletScript != null)
        {
            bulletScript.direction = shootPoint.forward;
            Debug.Log($"🔫 Disparo desde {shootPoint.position} con dirección {shootPoint.forward}");
        }
        else
        {
            Debug.LogError("❌ La bala no tiene el script Bullet3D.");
        }
    }
}
