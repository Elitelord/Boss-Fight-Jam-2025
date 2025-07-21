using UnityEngine;
using TMPro; // TextMeshPro support

public class move : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    [Header("Animator")]
    private Animator animator;
    private bool jumpTriggered = false;

    [Header("Debug UI (TMP)")]
    public TMP_Text speedText;
    public TMP_Text isJumpingText;
    public TMP_Text jumpText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleShooting();

        // Animator sync
        animator.SetBool("IsJumping", !isGrounded);

        // Debug UI display
        speedText.text = "Speed: " + animator.GetFloat("Speed").ToString("F2");
        isJumpingText.text = "IsJumping: " + animator.GetBool("IsJumping");
        jumpText.text = "Jump Triggered: " + jumpTriggered;
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip the player
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

        // Pass signed speed to animator
        animator.SetFloat("Speed", moveInput);
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
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPos - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = direction * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
