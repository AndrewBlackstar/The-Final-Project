using UnityEngine;

public class BouncingBullet : MonoBehaviour
{
    [Header("Variables")]
    public float speed = 1000f; // Velocidad inicial de la bala
    public float damage = 10f; // Daño que aplica la bala
    public int maxBounces = 3; // Número máximo de rebotes antes de desactivarse
    public float lifeTime = 5f; // Tiempo máximo de vida de la bala

    private Rigidbody rb;
    private int bounceCount = 0; // Contador de rebotes

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // Reiniciar variables
        bounceCount = 0;

        // Reinicializar completamente el Rigidbody
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Iniciar temporizador de vida útil
        Invoke("Deactivate", lifeTime);
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        // Establecer la posición y rotación iniciales
        transform.position = position;
        transform.rotation = rotation;

        // Calcular dirección de disparo basada en la rotación
        Vector3 shootDirection = transform.forward; // Dirección hacia adelante de la bala
        shootDirection.Normalize();

        // Aplicar fuerza inicial
        rb.AddForce(shootDirection * speed, ForceMode.Impulse);

        Debug.Log($"🚀 Bala inicializada desde {position} con dirección {shootDirection}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"💥 Bala impactó con: {collision.gameObject.name}");

        // Verificar si el objeto colisionado es un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var health = collision.gameObject.GetComponent<HealthManager>();
            if (health != null)
            {
                health.takeDamage(damage);
                Debug.Log($"🩸 Daño aplicado: {damage} a {collision.gameObject.name}");
            }
        }

        // Incrementar el contador de rebotes
        bounceCount++;

        // Desactivar la bala si supera el número máximo de rebotes
        if (bounceCount >= maxBounces)
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        // Cancelar cualquier invocación pendiente para evitar errores
        CancelInvoke();

        // Notificar al pool que la bala está lista para ser reutilizada
        GetComponent<BulletPool3D>()?.ReturnBullet(gameObject);

        // Desactivar la bala
        gameObject.SetActive(false);
    }
}