using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance; // Singleton instance for global access
    [SerializeField] private float baseSpeed = 10f;   // Base movement speed
    [SerializeField] public float currentSpeed;      // Current movement speed (visible in Inspector)
    [Header("Knockback and Jump Stats")]
    [SerializeField] private float baseKnockback = 1f; // Base knockback value
    [SerializeField] public float currentKnockback;   // Current knockback (visible in Inspector)
    [SerializeField] private float baseJumpHeight = 2f; // Base jump height value
    [SerializeField] public float currentJumpHeight;   // Current jump height (visible in Inspector)
    [SerializeField] private int foodPoints = 1;      // Initial food points (visible in Inspector)
    [SerializeField] private int drinkPoints = 1;     // Initial drink points (visible in Inspector)
    [SerializeField] private int armorPoints = 1;     // Initial armor points (visible in Inspector)
    [SerializeField] private int currency = 1000;     // Initial currency (visible in Inspector)
    [Header("Player Physics")]
    public float gravity = -9.81f;
    [Header("Scene Transition")]
    public Transform defaultSpawnPoint;               // Default spawn point if none specified for a scene
    public Transform[] sceneSpawnPoints;              // Array to assign spawn points for specific scenes

    void Awake()
    {
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
        currentSpeed = baseSpeed; 
        currentKnockback = baseKnockback; 
        currentJumpHeight = baseJumpHeight; 
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void Update()
    {
        Debug.Log($"Food Points: {foodPoints}, Base Speed: {baseSpeed}, Current Speed: {currentSpeed}, Armor: {armorPoints}, Drink: {drinkPoints}, Knockback: {currentKnockback}, Jump Height: {currentJumpHeight}");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;

        if (sceneIndex < sceneSpawnPoints.Length && sceneSpawnPoints[sceneIndex] != null)
        {
            transform.position = sceneSpawnPoints[sceneIndex].position;
            Debug.Log($"Player teleported to spawn point for scene: {scene.name}");
        }
        else if (defaultSpawnPoint != null)
        {
            transform.position = defaultSpawnPoint.position;
            Debug.Log($"Player teleported to default spawn point for scene: {scene.name}");
        }
        else
        {
            Debug.Log($"No spawn point specified. Player remains at current position in scene: {scene.name}");
        }

        ApplyStatsBasedOnPoints();
    }

    private void ApplyStatsBasedOnPoints()
    {
        ApplySpeedBasedOnFoodPoints();
        ApplyKnockbackBasedOnArmor();
        ApplyJumpHeightBasedOnDrinkPoints();
    }

    private void ApplySpeedBasedOnFoodPoints()
    {
        if (foodPoints == 0)
        {
            SetBaseSpeedToCurrentSpeed();
        }
        else if (foodPoints < 1)
        {
            ApplySpeedPenalty();
        }
        else if (foodPoints >= 1)
        {
            ApplySpeedBuff();
        }
    }

    private void ApplySpeedBuff()
    {
        baseSpeed = 20f;
        currentSpeed = baseSpeed;
        Debug.Log("Speed Buff applied. Base Speed Doubled. Current Speed: " + currentSpeed);
    }

    private void ApplySpeedPenalty()
    {
        baseSpeed = 5f;
        currentSpeed = baseSpeed;
        Debug.Log("Speed Penalty applied. Base Speed Halved. Current Speed: " + currentSpeed);
    }

    private void SetBaseSpeedToCurrentSpeed()
    {
        baseSpeed = currentSpeed;
        Debug.Log("Base Speed set to Current Speed: " + currentSpeed);
    }

    private void ApplyKnockbackBasedOnArmor()
    {
        if (armorPoints > 0)
        {
            currentKnockback = baseKnockback * 2f;
        }
        else if (armorPoints == 0)
        {
            currentKnockback = baseKnockback;
        }
        else
        {
            currentKnockback = baseKnockback / 2f;
        }
        Debug.Log("Knockback: " + currentKnockback);
    }

    private void ApplyJumpHeightBasedOnDrinkPoints()
    {
        if (drinkPoints > 0)
        {
            currentJumpHeight = baseJumpHeight * 2f;
        }
        else if (drinkPoints == 0)
        {
            currentJumpHeight = baseJumpHeight;
        }
        else
        {
            currentJumpHeight = baseJumpHeight / 2f;
        }
        Debug.Log("Jump Height: " + currentJumpHeight);
    }

    public void EndCourse()
    {
        foodPoints -= 1;
        armorPoints -= 1;
        drinkPoints -= 1;

        Debug.Log($"End of course. Food Points: {foodPoints}, Armor Points: {armorPoints}, Drink Points: {drinkPoints}");
        ApplyStatsBasedOnPoints();
    }

    public void AddFoodPoints(int amount)
    {
        foodPoints += amount;
        Debug.Log("Food purchased! Current Food Points: " + foodPoints);
    }

    public void AddDrinkPoints(int amount)
    {
        drinkPoints += amount;
        Debug.Log("Drink purchased! Current Drink Points: " + drinkPoints);
    }

    public void AddArmorPoints(int amount)
    {
        armorPoints += amount;
        Debug.Log("Armor purchased! Current Armor Points: " + armorPoints);
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        Debug.Log("Currency added. Current Currency: $" + currency);
    }

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

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public int GetFoodPoints()
    {
        return foodPoints;
    }

    public int GetArmorPoints()
    {
        return armorPoints;
    }

    public int GetDrinkPoints()
    {
        return drinkPoints;
    }

    public int GetCurrency()
    {
        return currency;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            AddCurrency(100);
            EndCourse();
        }
    }
}
