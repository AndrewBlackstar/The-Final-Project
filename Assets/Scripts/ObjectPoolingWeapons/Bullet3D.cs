using UnityEngine;

public class Bullet3D : MonoBehaviour
{
    [Header("Variables")]
    public float speed = 1000f; // Más alto porque AddForce usa fuerza (no velocidad directa)
    public float damage = 10f; // Daño que aplica la bala
    [SerializeField] private float angle; // Ángulo de disparo
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
        

        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySfx("shot pistol");
            // Aplicar daño al enemigo
            var health = collision.gameObject.GetComponent<HealthManager>();
            if (health != null)
            {
                health.takeDamage(damage);
                
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