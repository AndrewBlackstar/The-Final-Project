using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage = 100f;
    public float speed = 6f;
    public float lifetime = 5f;
    public float detectionRadius = 2f; // Distancia mínima para hacer daño
    public GameObject explosionEffect; // Asigna un prefab de explosión

    private Transform target;
    private bool isActive = false;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        isActive = true;
        CancelInvoke();
        Invoke(nameof(Deactivate), lifetime);
    }

    void Update()
    {
        if (!isActive || target == null) return;

        // Movimiento hacia el jugador
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;

        // Raycast corto para detectar si está cerca del player
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, detectionRadius))
        {
            if (hit.collider.CompareTag("Player"))
            {
                ExplodeAndDamage(hit.collider.gameObject);
            }
        }
    }

    private void ExplodeAndDamage(GameObject targetObject)
    {
        // Comprobar si el jugador está defendiendo
        PlayerCombat combat = targetObject.GetComponent<PlayerCombat>();
        if (combat == null)
        {
            // Si no está en este GameObject, buscar en el padre (por si el collider está en un hijo)
            combat = targetObject.GetComponentInParent<PlayerCombat>();
        }

        if (combat != null && combat.isMeleeDefending)
        {
            Debug.Log("🛡️ Daño bloqueado por ataque cuerpo a cuerpo");
            Deactivate(); // Aun así explotamos y desaparece
            return;
        }

        // Aplicar daño si no está bloqueando
        HealthManager health = targetObject.GetComponent<HealthManager>();
        if (health == null)
        {
            health = targetObject.GetComponentInParent<HealthManager>();
        }

        if (health != null)
        {
            Debug.Log("💥 Fireball explotó cerca del jugador e hizo daño");
            health.takeDamage(damage);
        }

        // Efecto de explosión
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExplodeAndDamage(other.gameObject);
        }
    }

    private void OnEnable()
    {
        isActive = false;
        CancelInvoke();
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void Deactivate()
    {
        FireballPool.instance.ReturnFireball(gameObject);
    }
}
