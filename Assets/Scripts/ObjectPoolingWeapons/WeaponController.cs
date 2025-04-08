using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public BulletPool3D bulletPool; // Pool de balas
    public Transform shootPoint; // Punto de disparo (debe asignarse en el Inspector)

    void Update()
    {
        if (shootPoint != null)
        {
            
            shootPoint.position = transform.position;
            shootPoint.rotation = transform.rotation;
        }
    }

    public void FireBullet()
    {
        if (bulletPool == null)
        {
            
            return;
        }

        if (shootPoint == null)
        {
            
            return;
        }

        
        GameObject bullet = bulletPool.GetBullet(shootPoint.position, shootPoint.rotation);

        if (bullet == null)
        {
            
            return;
        }

        
    }
}
