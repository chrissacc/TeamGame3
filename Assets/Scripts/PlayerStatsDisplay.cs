using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace

public class PlayerStatsDisplay : MonoBehaviour
{
    // Reference to the PlayerStats script. This will allow you to assign it in the Inspector.
    public PlayerStats playerStats;

    // Reference to the UI TextMeshProUGUI component where we will display the stats
    public TextMeshProUGUI statsText;

    void Start()
    {
        // Ensure PlayerStats is assigned
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in the inspector!");
        }

        // Ensure TextMeshProUGUI component is assigned
        if (statsText == null)
        {
            Debug.LogError("Stats TextMeshProUGUI is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Update the stats text with current values from PlayerStats
        UpdateStatsDisplay();
    }

    private void UpdateStatsDisplay()
    {
        // Create a string that contains all the stats to display
        string stats = "Currency: $" + playerStats.GetCurrency() + "\n";
        stats += "Food Points: " + playerStats.GetFoodPoints() + "\n";
        stats += "DrinkPoints: " + playerStats.GetDrinkPoints() + "\n";
        stats += "Armor Points: " + playerStats.GetArmorPoints() + "\n";

        // Update the TextMeshProUGUI component with the stats string
        statsText.text = stats;
    }
}