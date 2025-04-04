using UnityEngine;

public class Bullet3D : MonoBehaviour
{
    public float speed = 1000f; // Más alto porque AddForce usa fuerza (no velocidad directa)
    public float damage = 10f;
    public Vector3 direction;
    public System.Action destroyed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"💥 Bala impactó con: {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            var health = collision.gameObject.GetComponent<HealthManager>();
            if (health != null)
            {
                health.takeDamage(damage);
                Debug.Log($"🩸 Daño aplicado: {damage} a {collision.gameObject.name}");
            }
        }

        // Si no quieres que rebote infinitamente, puedes destruirla tras cierto tiempo o impactos
        Destroy(gameObject, 1f); // O usa pooling: gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        destroyed?.Invoke();
    }
}
