// Enemy.cs
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(float damageAmount)
    {
        // Subtract damage and convert to an integer
        health -= (int)damageAmount;
        Debug.Log(gameObject.name + " took " + damageAmount + " damage. Health is now " + health);

        // Check if the enemy has run out of health
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");
        // You can add explosion effects or sounds here before destroying it
        Destroy(gameObject);
    }
}
