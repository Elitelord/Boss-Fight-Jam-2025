using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool jumpRequest;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequest = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for Ground level triggers
        if (collision.gameObject.CompareTag("Ground1")) LevelManager.Instance.SetLevel(1);
        else if (collision.gameObject.CompareTag("Ground2")) LevelManager.Instance.SetLevel(2);
        else if (collision.gameObject.CompareTag("Ground3")) LevelManager.Instance.SetLevel(3);
        else if (collision.gameObject.CompareTag("Ground4")) LevelManager.Instance.SetLevel(4);
        else if (collision.gameObject.CompareTag("Ground5")) LevelManager.Instance.SetLevel(5);
    }
}
