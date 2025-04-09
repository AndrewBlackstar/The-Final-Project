using UnityEngine;
using System.Collections;

public class MomEnemyController : MonoBehaviour
{
    [Header("Movimiento")]
    public Transform player;
    public float moveSpeed = 2f;
    public float delayBeforeWalking = 2f;
    public float obstacleDetectionDistance = 1.5f;
    public float turnSpeed = 120f;
    public float walkDuration = 5f;

    [Header("Ataque")]
    public GameObject attackPrefab;
    public Transform firePoint;
    public float attackDuration = 5f;

    private float timer = 0f;
    private float walkTimer = 0f;
    private bool isWalking = false;
    private bool isAttacking = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", false);
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        if (!isWalking && !isAttacking && timer >= delayBeforeWalking)
        {
            StartWalking();
        }

        if (isWalking && !isAttacking)
        {
            walkTimer += Time.deltaTime;

            MoveTowardsPlayer();

            if (walkTimer >= walkDuration)
            {
                StopWalking();
                StartCoroutine(PerformAttack());
            }
        }
    }

    void StartWalking()
    {
        isWalking = true;
        walkTimer = 0f;
        animator.SetBool("isWalking", true);
    }

    void StopWalking()
    {
        isWalking = false;
        animator.SetBool("isWalking", false);
    }

    void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0;

        if (IsObstacleAhead())
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    bool IsObstacleAhead()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
        return Physics.Raycast(ray, obstacleDetectionDistance);
    }

    IEnumerator PerformAttack()
    {
        AudioManager.Instance.PlaySfx("slap");
        isAttacking = true;
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.3f);

        if (attackPrefab && firePoint)
        {
            GameObject projectile = Instantiate(attackPrefab, firePoint.position, Quaternion.identity);
            Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
            projectile.GetComponent<Projectile>().Initialize(directionToPlayer);
        }

        yield return new WaitForSeconds(attackDuration - 0.3f);

        isAttacking = false;
        timer = 0f;
        StartWalking();
    }
}