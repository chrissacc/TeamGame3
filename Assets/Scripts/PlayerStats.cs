using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance; // Singleton instance for global access

    public float baseSpeed = 10f;       // Base movement speed
    private float currentSpeed;         // Current movement speed
    [SerializeField] private int foodPoints = -5; // Initial food points (now visible in Inspector)
    [SerializeField] private int currency = 1000; // Initial currency (now visible in Inspector)
    public CharacterController controller; // Reference to CharacterController

    public float gravity = -9.81f;      // Gravity for the player
    public float jumpHeight = 2f;       // Jump height

    private Vector3 velocity;           // Player's velocity for gravity
    public Transform groundCheck;       // Transform to check if the player is grounded
    public float groundDistance = 0.4f; // Radius of ground check sphere
    public LayerMask groundMask;        // LayerMask to identify ground

    private bool isGrounded;            // Whether the player is grounded

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        currentSpeed = baseSpeed; // Initialize current speed
    }

    void Update()
    {
        HandleMovement();
    }

    // Deduct food points at the end of each course
    public void EndCourse()
    {
        foodPoints -= 1;
        Debug.Log("End of course. Food Points: " + foodPoints);

        // Apply penalties if food points reach 0 or less
        if (foodPoints <= 0)
        {
            ApplyPenalties();
        }
    }

    // Apply penalties to player stats
    private void ApplyPenalties()
    {
        // Halve the player's speed if food points are 0 or less
        currentSpeed = baseSpeed / 2f;
        Debug.Log("Penalties applied. Speed halved. Current Speed: " + currentSpeed);
    }

    // Add food points (e.g., after purchasing food)
    public void AddFoodPoints(int amount)
    {
        foodPoints += amount;
        Debug.Log("Food purchased! Current Food Points: " + foodPoints);
    }

    // Add currency to the player
    public void AddCurrency(int amount)
    {
        currency += amount;
        Debug.Log("Currency added. Current Currency: $" + currency);
    }

    // Deduct currency from the player
    public void DeductCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            Debug.Log("Currency deducted. Remaining Currency: $" + currency);
        }
        else
        {
            Debug.Log("Not enough currency to deduct!");
        }
    }

    // Get the player's current speed
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    // Get the player's current food points
    public int GetFoodPoints()
    {
        return foodPoints;
    }

    // Get the player's current currency
    public int GetCurrency()
    {
        return currency;
    }

    // 3D movement handling
    private void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ensure player stays grounded
        }

        // Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

