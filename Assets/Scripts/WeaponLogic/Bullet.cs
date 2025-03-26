using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala

    void Update()
    {
        // Aquí puedes agregar la lógica para mover la bala
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Aquí puedes agregar un comportamiento de colisión si lo deseas
    private void OnCollisionEnter(Collision collision)
    {
        // Lógica al colisionar
        gameObject.SetActive(false); // Desactivar la bala
    }
}
