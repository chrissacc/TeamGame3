using UnityEngine;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour
{
    // text that displays stats
    public TextMeshProUGUI statsText;

    void Start()
    {
        // make sure text works
        if (statsText == null)
        {
            Debug.LogError("Stats TextMeshProUGUI is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // playerstats is available
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
        // Create string contains stats to display
        string stats = "Currency: $" + PlayerStats.Instance.GetCurrency() + "\n";
        stats += "Food Points: " + PlayerStats.Instance.GetFoodPoints() + "\n";
        stats += "Drink Points: " + PlayerStats.Instance.GetDrinkPoints() + "\n";
        stats += "Armor Points: " + PlayerStats.Instance.GetArmorPoints() + "\n";

        // Update text
        statsText.text = stats;
    }
}