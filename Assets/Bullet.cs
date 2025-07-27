using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public int damage = 20;
    public float lifetime = 2f;
    public bool isFromEnemy = false;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFromEnemy)
        {
            // Enemy bullet hits player
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            // Player bullet hits enemy
            EnemyHealth1 enemy = collision.GetComponent<EnemyHealth1>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }

        // Destroy on contact with solid objects (not triggers)
        if (!collision.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
