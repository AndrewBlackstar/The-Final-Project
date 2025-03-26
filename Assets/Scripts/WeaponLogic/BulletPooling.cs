using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletPooling : MonoBehaviour
{
    public int poolSize; // Tamaño de la piscina de balas
    public GameObject bulletPrefab; // Prefab de la bala
    [SerializeField] List<GameObject> bulletsCharged = new List<GameObject>(); // Lista de balas en la piscina
    [SerializeField] Transform shootPoint; // Punto de disparo
    public GameObject bulletSelected; // Bala seleccionada para disparar

    void Start()
    {
        MakePool(poolSize); // Inicializa la piscina de balas
    }

    public void MakePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject bullet;
            bullet = Instantiate(bulletPrefab); // Instancia una bala
            bullet.SetActive(false); // La desactiva (no visible en la escena)
            bulletsCharged.Add(bullet); // La añade a la piscina
        }
    }

    // Método para "disparar" la bala
    public void FireBullets()
    {
        bulletSelected = UseBullet();
        bulletSelected.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation); // Establece la posición y rotación de la bala
        bulletSelected.SetActive(true); // Activa la bala para que aparezca
    }

    // Devuelve una bala desactivada de la piscina
    public GameObject UseBullet()
    {
        for (int i = 0; i < bulletsCharged.Count; i++)
        {
            if (!bulletsCharged[i].activeInHierarchy)
            {
                return bulletsCharged[i];
            }
        }

        // Si no hay balas disponibles, añade una nueva
        MakePool(1);
        return bulletsCharged.Last();
    }
}
