using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public float speed = 3f;  // Velocidad de movimiento
    public float attackRange = 2f;  // Distancia para atacar
    public float pushForce = 10f;
    public float attackCooldown = 0.5f; // Tiempo de espera entre golpes

    private Rigidbody enemyRb;
    private float lastAttackTime = 0f; // Guarda el último golpe

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time; // Registra el tiempo del golpe
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        enemyRb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        Debug.Log("Golpeando al jugador! Tiempo actual: " + Time.time);

        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        if (playerRb != null)
        {
            Vector3 pushDirection = (player.position - transform.position).normalized;
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
