using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyAI : EnemyDodge
{
    public GameObject throwablePrefab;
    public Transform throwPoint;
    public float throwCooldown = 3f;

    protected float lastThrowTime = 0f;
    private bool isThrowing = false;

    protected override void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (isThrowing) return;

        if (distance <= detectionRange && distance > attackRange)
        {
            if (Time.time >= lastThrowTime + throwCooldown)
            {
                StartCoroutine(ThrowSequence());
                lastThrowTime = Time.time;
            }

            animator.SetBool("isRunning", false);
        }
        else
        {
            base.Update(); // Movimiento y ataque cuerpo a cuerpo
        }

        if (Time.time >= lastDodgeTime + dodgeCooldown)
        {
            Dodge();
            lastDodgeTime = Time.time;
        }
    }

    IEnumerator ThrowSequence()
    {
        isThrowing = true;

        //animator.SetTrigger("crouch");
        yield return new WaitForSeconds(0.8f); // tiempo para la animación

        //animator.SetTrigger("throw");
        yield return new WaitForSeconds(0.6f); // tiempo antes de lanzar

        LaunchObject();

        yield return new WaitForSeconds(0.5f); // terminar animación
        isThrowing = false;
    }

    void LaunchObject()
    {
        if (throwablePrefab && throwPoint)
        {
            GameObject obj = Instantiate(throwablePrefab, throwPoint.position, throwPoint.rotation);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            Vector3 dir = (player.position - throwPoint.position).normalized;
            rb.linearVelocity = dir * 12f;

            Debug.Log(" Lanzamiento finalizado");
        }
    }
    public override void Die()
    {
        // Cambiar de escena usando el GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.LoadScene("cinematic 3"); 
        }
    }
}
