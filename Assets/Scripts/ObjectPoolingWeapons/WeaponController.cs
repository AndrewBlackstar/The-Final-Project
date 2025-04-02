using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public BulletPool3D bulletPool;
    public Transform shootPoint;

    void Start()
    {
        if (bulletPool == null)
        {
            bulletPool = FindFirstObjectByType<BulletPool3D>(); // Buscar pool si no está asignado
        }

        if (bulletPool == null)
        {
            Debug.LogError("No se encontró BulletPool3D en la escena.");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Botón de disparo (puedes cambiarlo)
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        if (bulletPool == null || shootPoint == null)
        {
            Debug.LogError("ERROR: bulletPool o shootPoint no asignados en " + gameObject.name);
            return;
        }

        GameObject bullet = bulletPool.GetBullet();
        if (bullet != null)
        {
            bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
            bullet.SetActive(true);
            Bullet3D bulletScript = bullet.GetComponent<Bullet3D>();
            if (bulletScript != null)
            {
                bulletScript.direction = shootPoint.forward;
            }
        }
    }
}
