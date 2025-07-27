using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float bulletSpeed = 20f;
    public Transform target;

    private float fireCooldown = 0f;

    void Update()
    {
        if (target == null) return;

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || target == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = (target.position - firePoint.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;

            // Optional debug line in Scene view
            Debug.DrawLine(firePoint.position, target.position, Color.red, 1f);
        }
    }
}
