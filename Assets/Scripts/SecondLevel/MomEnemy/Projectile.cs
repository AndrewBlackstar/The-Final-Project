using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f;
    public float speed = 10f;

    private Rigidbody rb;

    public void Initialize(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = direction.normalized * speed;

        // Opcional: orientar visualmente el proyectil hacia la dirección
        transform.rotation = Quaternion.LookRotation(direction);

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador impactado");
            Destroy(gameObject);
        }
    }
}
