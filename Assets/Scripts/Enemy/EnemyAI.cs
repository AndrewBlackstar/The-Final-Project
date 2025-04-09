using UnityEngine;
using System.Collections;

public class EnemyAI : EnemyDodge
{
    public Transform throwPoint;
    public float throwCooldown = 3f;
    public float minimumDistance = 7f;

    protected float lastThrowTime = 0f;
    private bool isThrowing = false;

    protected override void Update()
    {
        if (player == null || isThrowing)
        {
            animator.SetBool("isRunning", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            StopMoving();
            return;
        }

        if (distance > attackRange)
        {
            if (distance > minimumDistance)
                MoveTowardsPlayer();
            else
                StopMoving();

            if (!isAttacking && Time.time >= lastThrowTime + throwCooldown)
            {
                lastThrowTime = Time.time;
                StartCoroutine(AttackSequence());
            }
        }
        else
        {
            StopMoving();
        }
    }

    public override IEnumerator AttackSequence()
    {
        Debug.Log("🔥 Iniciando secuencia de ataque con fuego");

        isAttacking = true;
        isThrowing = true;

        animator.CrossFade("monster punch", 0.1f);

        yield return new WaitForSeconds(0.5f); // Tiempo para coincidir con animación

        LaunchFireball();

        yield return new WaitForSeconds(0.5f); // Espera antes de permitir otro ataque

        isThrowing = false;
        isAttacking = false;

        Debug.Log("✅ Ataque de fuego completado");
    }

    private void LaunchFireball()
    {
        if (throwPoint == null)
        {
            Debug.LogWarning("⚠️ throwPoint no está asignado en el inspector.");
            return;
        }

        Vector3 spawnPos = throwPoint.position + throwPoint.forward * 0.5f;
        Vector3 direction = (player.position - spawnPos).normalized;
        Quaternion rot = Quaternion.LookRotation(direction);

        GameObject fireball = FireballPool.instance.GetFireball(spawnPos, rot);

        // Ignorar colisiones con el enemigo
        Collider fireballCol = fireball.GetComponent<Collider>();
        Collider[] enemyColliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in enemyColliders)
        {
            if (fireballCol != null && col != null)
            {
                Physics.IgnoreCollision(fireballCol, col);
            }
        }

        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.SetTarget(player); // ← aquí está el cambio importante
        }

        Debug.Log("🔥 Fireball lanzada desde " + throwPoint.name);
    }

    public override void Die()
    {
        GameManager.instance.LoadScene("cinematic 3");
    }

}
