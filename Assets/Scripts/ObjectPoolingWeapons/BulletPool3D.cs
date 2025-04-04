using UnityEngine;
using System.Collections.Generic;

public class BulletPool3D : MonoBehaviour
{
    public int poolSize = 10;
    public GameObject bulletPrefab;
    private List<GameObject> bulletsCharged = new List<GameObject>();

    void Start()
    {
        MakePool(poolSize);
    }

    private void MakePool(int size)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("ERROR: No se ha asignado un prefab de bala en BulletPool3D.");
            return;
        }

        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletsCharged.Add(bullet);
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        foreach (var bullet in bulletsCharged)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                bullet.SetActive(true);

                // Reiniciar su Rigidbody si tiene uno
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                return bullet;
            }
        }

        // Si no hay balas disponibles, crear una nueva y agregarla al pool
        GameObject newBullet = Instantiate(bulletPrefab, position, rotation);
        newBullet.SetActive(false);
        bulletsCharged.Add(newBullet);
        return newBullet;
    }
}
