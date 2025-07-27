using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;          // Speed of the bullet
    public int damage = 20;            // Damage dealt to enemies
    public float lifetime = 2f;        // How long before the bullet is auto-destroyed
    public LayerMask enemyLayer;       // Layer for detecting enemies

    void Start()
    {
        // Destroy bullet after a set lifetime to avoid clutter
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move bullet forward along its local right direction
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object hit is on the enemy layer
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Attempt to deal damage to enemy
            EnemyHealth1 enemyHealth = collision.GetComponent<EnemyHealth1>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy bullet on impact
        }
        else if (!collision.isTrigger)
        {
            // If it hits a wall or other solid, destroy the bullet
            Destroy(gameObject);
        }
    }
}
