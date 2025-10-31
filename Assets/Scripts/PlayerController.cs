using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDoubleJump;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Animator animator; // Reference to Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get Animator component
    }

    void Update()
    {
        Move();
        Jump();
        Melee();
        Shoot();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("speed", Mathf.Abs(moveInput));
    }

    void Jump()
    {
        if (groundCheck == null) return; // Safety check

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                animator.SetTrigger("Jump");
            }
            else if (canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = false;
                animator.SetTrigger("Jump");
            }
        }
    }

    void Melee()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Melee");
            // Add melee logic here (e.g., damage enemies in range)
        }
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Shoot");
            // Add shooting logic here (e.g., instantiate projectile)
        }
    }
}
