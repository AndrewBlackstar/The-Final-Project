using UnityEngine;

public class EnemyDodge : EnemyBase
{
    public float dodgeCooldown = 3f;
    public float dodgeForce = 10f;
    protected float lastDodgeTime = 0f;

    public float threatDetectionRadius = 10f; // radio de detección anticipada
    public float threatDetectionAngle = 45f;

    public float invulnerabilityDuration = 0.5f;
    public bool isDodging = false;

    public LayerMask threatLayer;

    protected override void Update()
    {
        base.Update();
        DetectThreats(); // ahora escanea cada frame
    }

    private void DetectThreats()
    {
        if (Time.time < lastDodgeTime + dodgeCooldown || isDodging) return;

        Debug.Log("👀 Buscando amenazas cercanas...");

        Collider[] threats = Physics.OverlapSphere(transform.position, threatDetectionRadius, threatLayer);

        if (threats.Length == 0)
        {
            Debug.Log("❌ No hay amenazas en el radio.");
            return;
        }

        foreach (Collider threat in threats)
        {
            Debug.Log("🟠 Detectado: " + threat.name);

            if (!threat.CompareTag("bulletDuck") && !threat.CompareTag("Canon") && !threat.CompareTag("Sword"))
            {
                Debug.Log("⚠️ Objeto detectado no tiene un tag de amenaza: " + threat.tag);
                continue;
            }

            Rigidbody threatRb = threat.GetComponent<Rigidbody>();
            if (threatRb != null)
            {
                Vector3 toEnemy = (transform.position - threat.transform.position).normalized;
                Vector3 threatDir = threatRb.linearVelocity.normalized;

                float angle = Vector3.Angle(threatDir, toEnemy);
                Debug.Log($"📐 Ángulo de amenaza: {angle}");

                if (angle <= threatDetectionAngle)
                {
                    Debug.Log("✅ Amenaza válida detectada. Ejecutando dodge.");
                    Dodge();
                    break;
                }
                else
                {
                    Debug.Log("🔵 La amenaza no viene hacia el enemigo. Ángulo muy amplio.");
                }
            }
            else
            {
                Debug.Log("⚠️ Amenaza sin Rigidbody, igual esquivamos.");
                Dodge();
                break;
            }
        }
    }


    protected void Dodge()
    {
        if (isDodging) return;

        isDodging = true;

        bool dodgeLeft = Random.value > 0.5f;
        Vector3 dodgeDir = Vector3.Cross((player.position - transform.position).normalized, Vector3.up);
        if (!dodgeLeft) dodgeDir = -dodgeDir;

        enemyRb.AddForce(dodgeDir * dodgeForce, ForceMode.Impulse);

        animator.SetBool("isLeft", dodgeLeft);
        animator.SetTrigger("dodge");
        animator.Play(dodgeLeft ? "monster dodge left" : "monster dodge right");

        Debug.Log("🌀 Esquivando hacia " + (dodgeLeft ? "izquierda" : "derecha"));

        StartCoroutine(DodgeCooldown());
    }

    private System.Collections.IEnumerator DodgeCooldown()
    {
        lastDodgeTime = Time.time;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isDodging = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, threatDetectionRadius);
    }
}
