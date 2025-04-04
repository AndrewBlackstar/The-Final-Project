using UnityEngine;
using System.Collections.Generic;

public class EnemyBase : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float attackRange = 2f;
    public float detectionRange = 15f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;
    private float speed = 3f;

    private Rigidbody enemyRb;
    private float lastAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
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
                MoveTowardsPlayer();
            }
            else
            {
                // Realiza el ataque mientras el tiempo actual es mayor al tiempo de cooldown
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    lastAttackTime = Time.time;
                    AttackPlayer();
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

        // Aplica movimiento 
        enemyRb.linearVelocity = new Vector3(direction.x * speed, enemyRb.linearVelocity.y, direction.z * speed);

        // Hace que el enemigo gire hacia el jugador
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
            Debug.Log("💥 Golpeando al jugador");
        }

        //  Hacer daño al jugador 🔹
        if (player.TryGetComponent(out HealthManager healthManager))
        {
            healthManager.takeDamage(10f); 
            Debug.Log(" Daño causado al jugador");
        }
        else
        {
            Debug.LogError("No se encontró HealthManager en el jugador.");
        }

        animator.SetBool("isAttacking", true);
    }

}
