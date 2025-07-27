using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 25;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        move player = other.GetComponent<move>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
