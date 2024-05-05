using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID;  // Player ID to distinguish between Player 1 and Player 2
    public float speed = 5.0f;
    public float swingForce = 10f;  // Added force for swinging
    public float jumpForce = 10.0f;
    private Rigidbody2D rb;
    public LayerMask groundLayers;

    private string horizontalInput;
    private string jumpInput;
    private bool isGrounded;
    private bool facingRight = true;

    // Reference to the other player's controller to check their grounded status
    public PlayerController otherPlayerController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontalInput = "Player" + playerID + "_Horizontal";
        jumpInput = "Player" + playerID + "_Jump";
    }

    void Update()
    {
        // Handle movement
        float moveInput = Input.GetAxisRaw(horizontalInput);

        // Only apply swing when one player is grounded and the other is not, and not when both are falling
        if ((isGrounded && !otherPlayerController.isGrounded) || (!isGrounded && otherPlayerController.isGrounded))
        {
            // Apply swinging force when one is falling and the other is grounded
            if (!isGrounded)
            {
                rb.AddForce(new Vector2(moveInput * swingForce, 0), ForceMode2D.Force);
            }
        }
        else
        {
            // Normal movement logic when both are grounded or both are falling
            if (isGrounded || (!isGrounded && !otherPlayerController.isGrounded))
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }
        }

        // Flip player when moving left/right
        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Handle jumping
        if (Input.GetButtonDown(jumpInput) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
