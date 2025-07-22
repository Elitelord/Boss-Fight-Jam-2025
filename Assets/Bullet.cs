using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed; // Avoid clutter
        Destroy(gameObject, lifetime);
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Enemy"))
    //     {
    //         EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
    //         if (enemy != null)
    //         {
    //             enemy.TakeDamage();
    //         }

    //         Destroy(gameObject);
    //     }
    // }
}

