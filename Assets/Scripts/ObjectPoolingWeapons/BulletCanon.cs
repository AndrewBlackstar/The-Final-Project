using UnityEngine;

public class BulletCanon : MonoBehaviour
{
    [Header("Variables")]
    public float speed = 1000f; // Más alto porque AddForce usa fuerza (no velocidad directa)
    public float damage = 1f; // Daño reducido
    public float slowPercentage = 0.8f; // Porcentaje de ralentización (50% en este caso)
    public float slowDuration = 3f; // Duración del efecto de ralentización
    [SerializeField] private float angle;
    public Vector3 direction;
    public System.Action destroyed;

    [Header("Lifetime")]
    public float lifeTime = 5f; // Tiempo máximo de vida de la bala

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Vector3 shootDirection = Camera.main.transform.forward;
        shootDirection.Normalize();
        float radians = angle * Mathf.Deg2Rad;
        Vector3 horizontalDirection = new Vector3(shootDirection.x, 0, shootDirection.z).normalized;
        Vector3 forceDirection = (horizontalDirection * Mathf.Cos(radians)) + (Vector3.up * Mathf.Sin(radians));
        rb.AddForce(forceDirection * speed, ForceMode.Impulse);

        // Iniciar temporizador de vida útil
        Invoke("Deactivate", lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"💥 Bala impactó con: {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Aplicar daño
            var health = collision.gameObject.GetComponent<HealthManager>();
            if (health != null)
            {
                health.takeDamage(damage);
                Debug.Log($"🩸 Daño aplicado: {damage} a {collision.gameObject.name}");
            }

            // Aplicar efecto de ralentización
            var slowEffect = collision.gameObject.GetComponent<SlowEffect>();
            if (slowEffect != null)
            {
                slowEffect.ApplySlow(slowPercentage, slowDuration);
                Debug.Log($"⏳ Aplicando efecto de ralentización al enemigo: {collision.gameObject.name}");
            }
            else
            {
                Debug.LogWarning($"⚠️ El enemigo {collision.gameObject.name} no tiene el componente SlowEffect.");
            }
        }

        // Desactivar la bala al colisionar
        Deactivate();
    }

    private void Deactivate()
    {
        // Cancelar cualquier invocación pendiente para evitar errores
        CancelInvoke();

        // Notificar al pool que la bala está lista para ser reutilizada
        destroyed?.Invoke();

        // Desactivar la bala
        gameObject.SetActive(false);
    }
}