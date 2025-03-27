using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float speed = 3f;
    public float attackRange = 2f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;
    public bool canMove = true; 

    [Header("Object Interaction")]
    public float objectDetectionRange = 10f;
    public LayerMask layerMask;
    private Rigidbody enemyRb;
    private float lastAttackTime = 0f;
    private readonly List<string> validTags = new() { "objectSmall", "objectMedium", "objectBig" };
    private EnemyThrowManager throwManager;
    private Animator animator;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        throwManager = GetComponent<EnemyThrowManager>();
        animator = GetComponent<Animator>();

        //enemyRb.isKinematic = false;
        enemyRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation;

        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator en el enemigo.");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange && canMove) // Se mueve si canMove es true
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
        enemyRb.linearVelocity = new Vector3(direction.x * speed, enemyRb.linearVelocity.y, direction.z * speed);
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
    }
}

