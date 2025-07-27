using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackCooldown = 0.5f;
    public int knifeDamage = 50;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    private float lastAttackTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth1>()?.TakeDamage(knifeDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
