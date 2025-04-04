using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public BulletPool3D bulletPool; // Pool de balas
    public Transform shootPoint; // Punto de disparo (debe asignarse en el Inspector)

    void Update()
    {
        if (shootPoint != null)
        {
            // Opcional: Asegúrate de que el shootPoint siga la posición/rotación del arma
            shootPoint.position = transform.position;
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

        // Obtener una bala del pool con la posición y rotación del shootPoint
        GameObject bullet = bulletPool.GetBullet(shootPoint.position, shootPoint.rotation);

        if (bullet == null)
        {
            Debug.LogError("❌ No hay balas disponibles en el BulletPool.");
            return;
        }

        // Notificar que se ha disparado
        Debug.Log($"🔫 Disparo desde {shootPoint.position} con dirección {shootPoint.forward}");
    }
}
