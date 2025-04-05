using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    public bool hasHit;

    public Slider healthSlider;

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
        if (CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
        else if (CompareTag("Enemy"))
        {
            // Llamamos al método Die() del EnemyAI si el enemigo muere
            GetComponent<EnemyAI>()?.Die();
        }
    }

    
    
}
