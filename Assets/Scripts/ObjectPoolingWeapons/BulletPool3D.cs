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

    public GameObject GetBullet()
    {
        foreach (var bullet in bulletsCharged)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        // Si no hay balas disponibles, crear una nueva y agregarla al pool
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bulletsCharged.Add(newBullet);
        return newBullet;
    }
}
