using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IMovable
{
    [Header("Player Settings")]
    public Transform player;
    
    public float attackRange = 2f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;

    [Header("Object Interaction")]
    public float objectDetectionRange = 10f;
    public LayerMask layerMask;
    private Rigidbody enemyRb;
    //private NavMeshAgent agent;

    private float lastAttackTime = 0f;
    [SerializeField] float damageAmount;
    private readonly List<string> validTags = new() { "objectSmall", "objectMedium", "objectBig" };
    private EnemyThrowManager throwManager;
    private Animator animator;

    private HealthManager healthManager;

    public float Speed { get; set; } = 3f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //agent = GetComponent<NavMeshAgent>();
        enemyRb = GetComponent<Rigidbody>();
        throwManager = GetComponent<EnemyThrowManager>();
        animator = GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();
        
        enemyRb.isKinematic = false;
        enemyRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (healthManager != null && healthManager.currentHealth <= 0)
        {
            Die();
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
            FindAndPickObject();
        }
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            AttackPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        enemyRb.linearVelocity = new Vector3(direction.x * Speed, enemyRb.linearVelocity.y, direction.z * Speed);
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

        // Aplicar daño al jugador
        if (player.TryGetComponent(out HealthManager playerHealth))
        {
            playerHealth.takeDamage(damageAmount); // Daño fijo de 10 puntos (puedes ajustarlo según necesites)
        }

        animator.SetBool("isAttacking", true);
    }


    private void FindAndPickObject()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, objectDetectionRange, layerMask, QueryTriggerInteraction.UseGlobal);

        foreach (Collider obj in objects)
        {
            if (!validTags.Contains(obj.tag)) continue;
            throwManager.PickUpObject(obj.gameObject);
            break;
        }
        // animator.SetBool("trow", true);
    }

    public void Die()
    {
        // Cambiar de escena usando el GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.LoadScene("cinematic 3"); 
        }
    }
}

