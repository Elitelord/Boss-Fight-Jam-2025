using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHits = 5;
    private int currentHits = 0;

    public void TakeDamage()
    {
        currentHits++;
        Debug.Log("Enemy hit: " + currentHits);

        if (currentHits >= maxHits)
        {
            Die();
        }
    }

    void Die()
    {
        // Add animation or sound here if you want
        Destroy(gameObject);
    }
}
