using UnityEngine;
using System.Collections.Generic;

public class BulletPool3D : MonoBehaviour
{
    public static BulletPool3D Instance { get; private set; } // Singleton instance

    [Header("Pool Settings")]
    public int poolSize = 10;
    public GameObject bulletPrefab;
    private List<GameObject> bulletsCharged = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
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

            // Configurar el evento "destroyed" para devolver la bala al pool
            Bullet3D bulletScript = bullet.GetComponent<Bullet3D>();
            if (bulletScript != null)
            {
                bulletScript.destroyed += () => ReturnBullet(bullet);
            }
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

        // Si no hay balas disponibles, expandir el pool
        GameObject newBullet = Instantiate(bulletPrefab, position, rotation);
        newBullet.SetActive(false);

        // Configurar el evento "destroyed" para la nueva bala
        Bullet3D bulletScript = newBullet.GetComponent<Bullet3D>();
        if (bulletScript != null)
        {
            bulletScript.destroyed += () => ReturnBullet(newBullet);
        }

        bulletsCharged.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        if (bullet != null && bullet.activeInHierarchy)
        {
            bullet.SetActive(false);
        }
    }
}