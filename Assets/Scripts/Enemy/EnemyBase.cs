using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{

    [Header("Player Detection")]
    public float speed = 3f;
    public float attackRange = 2f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;
    public float detectionRange = 15f;
    public float objectDetectionRange = 10f;
    private Rigidbody enemyRb;
    public Transform player;
    private Animator animator;
    private float lastAttackTime = 0f;
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

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
            //FindAndPickObject();
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
        animator.SetBool("isAttacking", true);
    }

}
