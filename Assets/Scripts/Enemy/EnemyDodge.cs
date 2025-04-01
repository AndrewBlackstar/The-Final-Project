using UnityEngine;

public class EnemyBaseWithDodge : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float attackRange = 2f;
    public float detectionRange = 15f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;
    private float speed = 10f;
    
    private Rigidbody enemyRb;
    private float lastAttackTime = 0f;
    private Animator animator;

    [Header("Dodge Settings")]
    public float dodgeDistance = 5f;  // Distancia de esquive
    public float dodgeCooldown = 2f;  // Tiempo entre esquives
    private float lastDodgeTime = 0f; // Último tiempo de esquive

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        enemyRb.isKinematic = false;
        enemyRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange) // Solo se mueve si el jugador está dentro del rango de detección
        {
            if (distance > attackRange)
            {
                MoveTowardsPlayer(); // Moverse hacia el jugador normalmente
            }
            else
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    lastAttackTime = Time.time;
                    AttackPlayer();
                }

                // Llamar a la función de esquivar si el enemigo está cerca del jugador y puede esquivar
                if (Time.time >= lastDodgeTime + dodgeCooldown)
                {
                    Dodge(); // Esquivar si es el momento
                }
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Aplicar movimiento normal (modificado para evitar sobreescribir el código)
        enemyRb.linearVelocity = new Vector3(direction.x * speed, enemyRb.linearVelocity.y, direction.z * speed);

        // Hacer que el enemigo gire hacia el jugador
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        animator.SetBool("isRunning", true);
    }

    private void AttackPlayer()
    {
        if (player.TryGetComponent(out Rigidbody playerRb))
        {
            Vector3 pushDirection = (player.position - transform.position).normalized;
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            Debug.Log("Golpeando al jugador");
        }

        animator.SetBool("isAttacking", true);
    }

    private void Dodge()
    {
        // Determinar si se mueve hacia la derecha o hacia la izquierda
        int direction = Random.Range(0, 2) == 0 ? 1 : -1;  // 1 para derecha, -1 para izquierda

        // Llamar a MoveTowardsPlayer, pero en vez de moverse hacia el jugador, mover lateralmente
        Vector3 dodgeDirection = transform.right * direction * dodgeDistance;  // Esquivar lateralmente

        // Desplazar al enemigo lateralmente usando el mismo código de movimiento
        enemyRb.linearVelocity = new Vector3(dodgeDirection.x, enemyRb.linearVelocity.y, 0f);

        lastDodgeTime = Time.time; // Actualizar el tiempo del último esquive
        animator.SetTrigger("Dodge"); // Activar animación de esquive (si existe)
        Debug.Log("Esquivando");
    }
}
