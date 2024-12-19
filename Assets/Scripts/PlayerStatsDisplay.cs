using UnityEngine;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour
{
    // Reference to the UI TextMeshProUGUI component where we will display the stats
    public TextMeshProUGUI statsText;

    void Start()
    {
        // Ensure TextMeshProUGUI component is assigned
        if (statsText == null)
        {
            Debug.LogError("Stats TextMeshProUGUI is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Ensure PlayerStats.Instance is available before updating the display
        if (PlayerStats.Instance != null)
        {
            UpdateStatsDisplay();
        }
        else
        {
            Debug.LogWarning("PlayerStats instance is not available.");
        }
    }

    private void UpdateStatsDisplay()
    {
        // Create a string that contains all the stats to display
        string stats = "Currency: $" + PlayerStats.Instance.GetCurrency() + "\n";
        stats += "Food Points: " + PlayerStats.Instance.GetFoodPoints() + "\n";
        stats += "Drink Points: " + PlayerStats.Instance.GetDrinkPoints() + "\n";
        stats += "Armor Points: " + PlayerStats.Instance.GetArmorPoints() + "\n";

        // Update the TextMeshProUGUI component with the stats string
        statsText.text = stats;
    }
}