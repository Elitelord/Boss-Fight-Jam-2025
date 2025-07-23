// Bullet.cs
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // This will be set by the weapon that fires it
    public float lifetime = 5f;

    void Awake()
    {
        // Destroy the bullet after a certain time to clean up the scene
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet hit an object with the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the Enemy script from the object we hit
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Call the TakeDamage method on the enemy
                enemy.TakeDamage(damage);
            }
        }

        // Destroy the bullet on impact with anything (except maybe the player)
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}