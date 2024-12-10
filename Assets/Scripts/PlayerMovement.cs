using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public CharacterController controller;
    public float gravity = -9.81f;         // Gravity for the player
    public float jumpHeight = 2f;          // Jump height
    private float currentSpeed;            // Current movement speed

    private Vector3 velocity;              // Velocity for gravity calculations
    public Transform groundCheck;          // Ground check position
    public float groundDistance = 0.4f;    // Radius for ground check
    public LayerMask groundMask;           // Mask to detect ground layers
    private bool isGrounded;               // Is player grounded?

    void Start()
    {
        currentSpeed = PlayerStats.Instance.currentSpeed; // Get initial speed from PlayerStats
    }

    void Update()
    {
        HandleMovement();
    }

    // Set the movement speed from the PlayerStats script
    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

    private void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset falling velocity when grounded
        }

        // Get input for movement using arrow keys
        float moveX = 0f;
        float moveZ = 0f;

        // Arrow keys input for movement
        if (Input.GetKey(KeyCode.UpArrow)) moveZ = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveZ = -1f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        // Calculate movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement using current speed
        controller.Move(move * (currentSpeed * Time.deltaTime));

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
