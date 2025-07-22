using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 baseScale;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    [SerializeField] private Transform weaponHoldPoint;
    public GameObject startingWeaponPrefab;
    private Weapon currentWeapon;
    private float nextFireTime = 0f;

    [Header("Animator")]
    private Animator animator;
    private bool jumpTriggered = false;

    [Header("Debug UI (TMP)")]
    public TMP_Text speedText;
    public TMP_Text isJumpingText;
    public TMP_Text jumpText;
    public TMP_Text healthText; // 👈 NEW: Health display

    [Header("Death Settings")]
    [SerializeField] private LayerMask killerObstacleLayer;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public int damagePerHit = 35;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (startingWeaponPrefab != null)
        {
            EquipWeapon(startingWeaponPrefab);
        }
        baseScale = transform.localScale;

        currentHealth = maxHealth;
        UpdateHealthDisplay();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleShooting();

        animator.SetBool("IsJumping", !isGrounded);

        speedText.text = "Speed: " + animator.GetFloat("Speed").ToString("F2");
        isJumpingText.text = "IsJumping: " + animator.GetBool("IsJumping");
        jumpText.text = "Jump Triggered: " + jumpTriggered;
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);
        else if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            jumpTriggered = true;
            Invoke(nameof(ResetJumpTriggerDisplay), 0.2f);
            isGrounded = false;
        }
    }

    void ResetJumpTriggerDisplay()
    {
        jumpTriggered = false;
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon != null && Time.time >= nextFireTime)
            {
                currentWeapon.Shoot();
                nextFireTime = Time.time + currentWeapon.weaponData.fireRate;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (((1 << collision.gameObject.layer) & killerObstacleLayer) != 0)
        {
            TakeDamage(damagePerHit);
        }
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent negative values
        Debug.Log("Took damage! Current health: " + currentHealth);

        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // animator.SetTrigger("Die");
        // GetComponent<move>().enabled = false;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        GameObject newWeaponObject = Instantiate(weaponPrefab, weaponHoldPoint.position, weaponHoldPoint.rotation);
        newWeaponObject.transform.SetParent(weaponHoldPoint);

        currentWeapon = newWeaponObject.GetComponent<Weapon>();
        Debug.Log("Equipped weapon: " + newWeaponObject.name); 
    }
}
