using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance; // Singleton instance for global access

    [SerializeField] private float baseSpeed = 10f;   // Base movement speed
    [SerializeField] public float currentSpeed;      // Current movement speed (visible in Inspector)
    [SerializeField] private int foodPoints = 1;      // Initial food points (visible in Inspector)
    [SerializeField] private int currency = 1000;     // Initial currency (visible in Inspector)
    public CharacterController controller;            // Reference to CharacterController

    [Header("Player Physics")] // Add grouping in Inspector for clarity
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    [Header("Scene Transition")]
    public Transform defaultSpawnPoint;               // Default spawn point if none specified for a scene
    public Transform[] sceneSpawnPoints;              // Array to assign spawn points for specific scenes

    private Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool isGrounded;

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
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
    }

    void Update()
    {
        HandleMovement();

        // Debug food points and speed
        Debug.Log($"Food Points: {foodPoints}, Base Speed: {baseSpeed}, Current Speed: {currentSpeed}");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene loaded event
    }

    // Scene loaded handler
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;

        // Check if a spawn point is assigned for this scene
        if (sceneIndex < sceneSpawnPoints.Length && sceneSpawnPoints[sceneIndex] != null)
        {
            transform.position = sceneSpawnPoints[sceneIndex].position;
            Debug.Log($"Player teleported to spawn point for scene: {scene.name}");
        }
        else if (defaultSpawnPoint != null)
        {
            // Use default spawn point if none is assigned for this scene
            transform.position = defaultSpawnPoint.position;
            Debug.Log($"Player teleported to default spawn point for scene: {scene.name}");
        }
        else
        {
            // Keep player at their current position
            Debug.Log($"No spawn point specified. Player remains at current position in scene: {scene.name}");
        }
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
        else
        {
            ResetSpeed();
        }
    }

    // Apply penalties to player stats
    private void ApplyPenalties()
    {
        baseSpeed /= 2f;
        currentSpeed = baseSpeed;
        Debug.Log("Penalties applied. Base Speed halved. Current Speed: " + currentSpeed);
    }

    // Reset speed to original base speed when penalties are lifted
    private void ResetSpeed()
    {
        baseSpeed = 10f; // Reset base speed to default
        currentSpeed = baseSpeed;
        Debug.Log("Speed reset. Base Speed: " + baseSpeed);
    }

    // Add food points
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

        // Apply movement using currentSpeed
        controller.Move(move * (currentSpeed * Time.deltaTime));

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Goal collision handling
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            // Add currency when reaching the goal
            AddCurrency(100); // Player gains 100 currency on goal collision

            // Deduct food points when reaching the goal
            foodPoints -= 1;  // Reduces food points

            // Log the results
            Debug.Log($"Goal reached! Currency Gained: $100, Food Points Lost: 1");
        }
    }
}
