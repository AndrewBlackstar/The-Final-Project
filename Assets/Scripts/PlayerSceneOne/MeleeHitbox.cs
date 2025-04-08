using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    public int meleeDamage = 200;
    [SerializeField] ParticleSystem MeleeVfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var health = other.GetComponent<HealthManager>();
            if (health != null)
            {
                MeleeVfx.Play();
                health.takeDamage(meleeDamage);
                Debug.Log($"🗡️ Golpe cuerpo a cuerpo a {other.name}, daño: {meleeDamage}");

                // 🎵 Sonido de golpe
                AudioManager.Instance.PlaySfx("slap");
            }
        }
    }
}
