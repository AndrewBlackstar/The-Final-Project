using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    public bool hasHit;

    public Slider healthSlider;

     private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }


    public void takeDamage(float damageAmmount)
    {
        if (TryGetComponent<EnemyDodge>(out var dodge) && dodge.isDodging)
        {
            Debug.Log("🛡️ Daño evitado por dodge");
            return;
        }

        if (isDead) return; // Evita que tome daño muerto
        currentHealth -=damageAmmount;

        if(healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        if (isDead) return; // Protege de doble muerte
        isDead = true;
        
        if (CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
        else if (CompareTag("Enemy"))
        {
            GetComponent<EnemyBase>()?.Die();

        }
    }

    
    
}
