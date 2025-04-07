using UnityEngine;


public class EnemyDodge : EnemyBase
{
    public float dodgeCooldown = 3f;
    public float dodgeForce = 10f;
    protected float lastDodgeTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (Time.time >= lastDodgeTime + dodgeCooldown)
        {
            Dodge();
            lastDodgeTime = Time.time;
        }
    }
    protected void Dodge()
    {
        Vector3 dodgeDir = Vector3.Cross((player.position - transform.position).normalized, Vector3.up);
        enemyRb.AddForce(dodgeDir * dodgeForce, ForceMode.Impulse);
        //animator.SetTrigger("");
        Debug.Log("Esquivando");
    }
}
