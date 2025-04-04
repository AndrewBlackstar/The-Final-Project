using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public BulletPool3D bulletPool;
    public Transform shootPoint;

    void Update()
    {
        if (shootPoint != null)
        {
            shootPoint.position = transform.position; // Opcional: Puedes usar el bone del arma
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

        // Obtener la bala del pool con la rotación corregida
        GameObject bullet = bulletPool.GetBullet(shootPoint.position, Quaternion.LookRotation(shootPoint.forward));

        if (bullet == null)
        {
            Debug.LogError("❌ No hay balas disponibles en el BulletPool.");
            return;
        }

        // Ajustar la rotación manualmente si es necesario
        bullet.transform.Rotate(0f, 90f, 0f); // Cambia este valor si sigue mal

        Bullet3D bulletScript = bullet.GetComponent<Bullet3D>();
        if (bulletScript != null)
        {
            bulletScript.direction = bullet.transform.forward;
            Debug.Log($"🔫 Disparo desde {shootPoint.position} con dirección {bullet.transform.forward}");
        }
        else
        {
            Debug.LogError("❌ La bala no tiene el script Bullet3D.");
        }
    }
}
