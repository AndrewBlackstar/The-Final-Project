using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName; // Nombre del arma
    public int damage; // Daño del arma
    public BulletPooling bulletPoolingScript; // Referencia al script BulletPooling

    // Método para disparar la arma
    public void Fire(Transform shootPoint)
    {
        // Llamamos al script BulletPooling para disparar la bala
        bulletPoolingScript.FireBullets();
    }
}
