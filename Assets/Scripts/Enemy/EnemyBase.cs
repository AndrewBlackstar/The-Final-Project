using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 3.5f;
    public float attackRange = 2f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;
    public float detectionRange = 15f;

    protected NavMeshAgent agent;  // ✅ Cambiamos Rigidbody por NavMeshAgent
    protected Transform player;
    protected Animator animator;
    protected float lastAttackTime = 0f;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.speed = speed;
        agent.stoppingDistance = attackRange - 0.5f;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró el jugador en la escena. ");
        }
    }

    protected virtual void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange && distance <= detectionRange)
        {
            MoveTowardsPlayer();
        }
        else if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    protected void MoveTowardsPlayer()
    {
        /* if (agent.isStopped) agent.isStopped = false;

         Debug.Log("Intentando mover al enemigo hacia " + player.position);

         agent.SetDestination(player.position);

         if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
         {
             Debug.LogError("El camino es inválido. ¿Está el enemigo en el NavMesh?");
         }

         if (agent.velocity.magnitude > 0.1f)
         {
             animator?.SetBool("isRunning", true);
         }
         else
         {
             animator?.SetBool("isRunning", false);
             Debug.Log("El enemigo no se está moviendo. Velocidad actual: " + agent.velocity.magnitude);
         }*/
        if (agent == null || player == null) return;

        agent.SetDestination(player.position);
        Debug.Log("Moviendo al enemigo hacia: " + player.position);
    }

    protected virtual void AttackPlayer()
    {
        if (player.TryGetComponent(out Rigidbody playerRb))
        {
            Vector3 pushDirection = (player.position - transform.position).normalized;
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            Debug.Log("Golpeando al jugador");
        }

        animator?.SetBool("isAttacking", true);
        agent.isStopped = true;  // ✅ Detener movimiento mientras ataca
    }
}
