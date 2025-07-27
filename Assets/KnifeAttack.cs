using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum WeaponType { Knife, Gun, Shield }
    public WeaponType currentWeapon = WeaponType.Knife;

    [Header("Knife Settings")]
    public float knifeRange = 1f;
    public float knifeCooldown = 0.5f;
    public int knifeDamage = 50;
    public Transform knifeAttackPoint;
    public LayerMask enemyLayer;

    [Header("Gun Settings")]
    public GameObject bulletPrefab;
    public Transform gunFirePoint;
    public float gunCooldown = 0.25f;

    [Header("Shield Settings")]
    public GameObject shieldObject;

    private float lastKnifeTime;
    private float lastGunTime;

    void Update()
    {
        // Swap weapon with V
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchWeapon();
        }

        switch (currentWeapon)
        {
            case WeaponType.Knife:
                if (Input.GetKeyDown(KeyCode.F) && Time.time >= lastKnifeTime + knifeCooldown)
                {
                    KnifeAttack();
                    lastKnifeTime = Time.time;
                }
                break;

            case WeaponType.Gun:
                if (Input.GetMouseButtonDown(0) && Time.time >= lastGunTime + gunCooldown)
                {
                    GunAttack();
                    lastGunTime = Time.time;
                }
                break;

            case WeaponType.Shield:
                // Shield is only active while holding or pressing F
                bool shieldActive = Input.GetKey(KeyCode.F);
                if (shieldObject != null)
                    shieldObject.SetActive(shieldActive);
                break;
        }
    }

    void SwitchWeapon()
    {
        int next = ((int)currentWeapon + 1) % System.Enum.GetNames(typeof(WeaponType)).Length;
        currentWeapon = (WeaponType)next;

        Debug.Log("Switched to: " + currentWeapon);

        // Turn off shield immediately when switching away
        if (currentWeapon != WeaponType.Shield && shieldObject != null)
        {
            shieldObject.SetActive(false);
        }
    }

    void KnifeAttack()
    {
        if (knifeAttackPoint == null) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(knifeAttackPoint.position, knifeRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth1>()?.TakeDamage(knifeDamage);
        }

        Debug.Log("Knife attack!");
    }

    void GunAttack()
    {
        if (bulletPrefab == null || gunFirePoint == null) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - gunFirePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        Instantiate(bulletPrefab, gunFirePoint.position, rotation);

        Debug.Log("Gun fired toward: " + direction);
    }

    void OnDrawGizmosSelected()
    {
        if (knifeAttackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(knifeAttackPoint.position, knifeRange);
        }
    }
}
