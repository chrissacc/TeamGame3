using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Rigidbody rb;                // Reference to the Rigidbody
    public Transform groundCheck;       // Ground check position
    public LayerMask groundMask;        // Mask to detect ground layers

    public float gravity = -9.81f;      // Gravity value
    public float groundDistance = 0.4f; // Radius for ground check
    private Vector3 velocity;           // Velocity for gravity calculations

    private bool isGrounded;            // Is the player grounded?

    private PlayerStats playerStats;    // Reference to the PlayerStats script
    private float currentSpeed;         // Player's current speed (updated dynamically)

    // Speed reduction when colliding with a StickyWall
    private float reducedSpeed;         // Speed while on StickyWall
    private bool isOnStickyWall = false; // Is the player on a sticky wall?

    void Start()
    {
        // Initialize the reference to PlayerStats
        playerStats = PlayerStats.Instance;

        // Ensure the Rigidbody is properly assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        // Set the initial speed to currentSpeed from PlayerStats
        currentSpeed = playerStats.currentSpeed;
        reducedSpeed = currentSpeed * 0.5f; // Example slow-down factor
    }

    void Update()
    {
        HandleMovement(); // Handle player movement
    }

    private void HandleMovement()
    {
        // Ground check using a small sphere radius
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset falling velocity when grounded
        }

        // Movement input
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) moveZ = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveZ = -1f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        // Calculate movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement using the current speed (which may be modified by StickyWall)
        rb.MovePosition(rb.position + move * (currentSpeed * Time.deltaTime));

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerStats.currentJumpHeight * -2f * gravity); // Use PlayerStats jump height
        }

        // Apply gravity using Rigidbody
        velocity.y += gravity * Time.deltaTime;
        rb.MovePosition(rb.position + velocity * Time.deltaTime);

        // Knockback logic
        if (playerStats.currentKnockback > 0f && !isGrounded)
        {
            Vector3 knockbackForce = transform.forward * playerStats.currentKnockback; // Apply knockback
            rb.MovePosition(rb.position + knockbackForce * Time.deltaTime);

            // Reduce knockback over time
            playerStats.currentKnockback = Mathf.Max(playerStats.currentKnockback - Time.deltaTime * 5f, 0f);
        }
    }

    // Method to reduce speed when colliding with StickyWall
    public void OnStickyWallEnter()
    {
        isOnStickyWall = true;
        currentSpeed = reducedSpeed; // Set speed to reduced value
    }

    // Method to restore normal speed when exiting StickyWall
    public void OnStickyWallExit()
    {
        isOnStickyWall = false;
        currentSpeed = playerStats.currentSpeed; // Restore original speed
    }
}

