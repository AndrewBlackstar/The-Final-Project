using UnityEngine;

public class EnemyDodge : EnemyBase
{
    public float dodgeCooldown = 3f;
    public float dodgeForce = 10f;
    protected float lastDodgeTime = 0f;

    public float threatDetectionAngle = 45f; // Margen de ángulo permitimos para detectar la amenaza

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < lastDodgeTime + dodgeCooldown) return;

        if (other.CompareTag("bulletDuck") || other.CompareTag("canon") || other.CompareTag("sword"))
        {
            Rigidbody threatRb = other.GetComponent<Rigidbody>();
            if (threatRb != null)
            {
                Vector3 toEnemy = (transform.position - other.transform.position).normalized;
                Vector3 threatDir = threatRb.velocity.normalized;

                float angle = Vector3.Angle(threatDir, toEnemy);

                if (angle <= threatDetectionAngle)
                {
                    Dodge();
                    lastDodgeTime = Time.time;
                }
                else
                {
                    Debug.Log("No esquiva porque no viene directo hacia él");
                }
            }
            else
            {
                // Si no tiene Rigidbody igual esquiva (por si es un ataque cuerpo a cuerpo)
                Dodge();
                lastDodgeTime = Time.time;
            }
        }
    }

    protected void Dodge()
    {
        bool dodgeLeft = Random.value > 0.5f;

        Vector3 dodgeDir = Vector3.Cross((player.position - transform.position).normalized, Vector3.up);
        if (!dodgeLeft) dodgeDir = -dodgeDir;

        enemyRb.AddForce(dodgeDir * dodgeForce, ForceMode.Impulse);

        animator.SetBool("isLeft", dodgeLeft);
        animator.SetTrigger("dodge");

        Debug.Log("Esquivando hacia " + (dodgeLeft ? "izquierda" : "derecha"));
    }
}
