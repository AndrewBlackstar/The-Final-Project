using UnityEngine;

public class EnemyBase : MonoBehaviour, IMovable
{
    [Header("Player Settings")]
    public Transform player;
    public float attackRange = 2f;
    public float detectionRange = 15f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;

    protected Rigidbody enemyRb;
    protected float lastAttackTime = 0f;
    protected Animator animator;

    public virtual float Speed { get; set; } = 3f;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        enemyRb.isKinematic = false;
        enemyRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    protected virtual void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance > attackRange)
            {
                MoveTowardsPlayer();
            }
            else if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                AttackPlayer();
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        enemyRb.linearVelocity = new Vector3(direction.x * Speed, enemyRb.linearVelocity.y, direction.z * Speed);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        animator.SetBool("isRunning", true);
    }

    protected virtual void AttackPlayer()
    {
        if (player.TryGetComponent(out Rigidbody playerRb))
        {
            Vector3 pushDirection = (player.position - transform.position).normalized;
            playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }

        if (player.TryGetComponent(out HealthManager healthManager))
        {
            healthManager.takeDamage(10f);
        }

        animator.SetTrigger("attack");
    }
}
