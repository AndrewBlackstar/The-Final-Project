using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IMovable
{
    [Header("Player Settings")]
    public Transform player;
    private HealthManager playerHealth;
    public float attackRange = 2f;
    public float detectionRange = 15f;
    public float pushForce = 5f;
    public float attackCooldown = 1f;
    protected bool isAttacking = false;

    protected Rigidbody enemyRb;
    protected float lastAttackTime = 0f;
    protected Animator animator;

    [SerializeField] private string nextZoneTag = "zoneMid";
    protected List<GameObject> nextZones = new List<GameObject>();

    public virtual float Speed { get; set; } = 3f;

    protected virtual void Awake()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag(nextZoneTag) && obj.hideFlags == HideFlags.None && obj.scene.IsValid())
            {
                nextZones.Add(obj);
            }
        }

        if (nextZones.Count == 0)
        {
            Debug.LogWarning("❌ No se encontraron zonas con el tag: " + nextZoneTag);
        }
        else
        {
            Debug.Log($"✅ Se encontraron {nextZones.Count} zonas con el tag '{nextZoneTag}'.");
        }
    }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerHealth = player.GetComponent<HealthManager>();

        enemyRb.isKinematic = false;
        enemyRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    protected virtual void Update()
    {
        if (player == null || isAttacking)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            // Muy lejos, solo Idle
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
            enemyRb.linearVelocity = Vector3.zero;
            return;
        }

        if (distance > attackRange)
        {
            // Dentro del rango de detección, pero aún lejos para atacar
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
            MoveTowardsPlayer();
        }
        else
        {
            // Dentro del rango de ataque
            animator.SetBool("isRunning", false);

            if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(AttackSequence());
            }
        }
    }



    protected void StopMoving()
    {
        enemyRb.linearVelocity = new Vector3(0, enemyRb.linearVelocity.y, 0);
        animator.SetBool("isRunning", false);
    }


    protected virtual void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position);
        direction.y = 0;
        direction.Normalize();

        Vector3 movement = direction * Speed;
        enemyRb.linearVelocity = new Vector3(movement.x, enemyRb.linearVelocity.y, movement.z);

        animator.SetBool("isRunning", true); // ✅ Aquí se asegura que se anime
        animator.SetBool("isAttacking", false);

        if (direction.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }



    public virtual IEnumerator AttackSequence()
    {
        isAttacking = true;

        // Forzamos la animación directamente
        animator.CrossFade("monster punch", 0.1f);

        float attackDuration = GetAnimationLength("monster punch");

        yield return new WaitForSeconds(attackDuration * 0.3f);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange + 0.5f)
        {
            if (playerHealth != null && distance <= attackRange + 0.5f)
            {
                playerHealth.takeDamage(45f);
                Debug.Log("💢 El enemigo hizo daño al jugador.");
            }

            if (player.TryGetComponent(out Rigidbody playerRb))
            {
                Vector3 pushDirection = (player.position - transform.position).normalized;
                playerRb.AddForce(pushDirection * (pushForce * 0.2f), ForceMode.Impulse);
            }
        }

        yield return new WaitForSeconds(attackDuration * 0.7f);

        // Ya no hace falta apagar nada si usamos CrossFade o HasExitTime
        isAttacking = false;
        lastAttackTime = Time.time;
    }



    public float GetAnimationLength(string animationName)
    {
        if (animator == null) return 0f;

        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (var clip in ac.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"⚠️ No se encontró el clip '{animationName}' en el Animator.");
        return 1f; // Duración por defecto si no lo encuentra
    }




    public virtual void Die()
    {
        // Activa la animación
        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        foreach (var zone in nextZones)
        {
            if (zone != null)
            {
                zone.SetActive(true);
            }
        }

        StartCoroutine(DeathDelay());
    }

    private System.Collections.IEnumerator DeathDelay()
    {
        // Espera a que termine la animación
        yield return new WaitForSeconds(4f); 
        Destroy(gameObject);
    }
}
