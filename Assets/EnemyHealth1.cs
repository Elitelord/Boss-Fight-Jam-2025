using UnityEngine;
using TMPro;

public class EnemyHealth1 : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = currentHealth.ToString();
    }

    void Die()
    {
        Destroy(gameObject);
        EndGame();
    }

    void EndGame()
    {
        // This will pause the entire game
        Time.timeScale = 0f;

        // Optional: Show a message on screen (add a public TMP text for this if desired)
        Debug.Log("Enemy defeated. Game Over.");
    }
}
