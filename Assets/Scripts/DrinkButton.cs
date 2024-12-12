using UnityEngine;

public class DrinkButton : MonoBehaviour
{
    [SerializeField] private int currencyCost = 50; // Amount of currency required to buy 1 food point

    public void AddDrinkPoint()
    {
        if (PlayerStats.Instance != null)
        {
            // Check if the player has enough currency
            if (PlayerStats.Instance.GetCurrency() >= currencyCost)
            {
                PlayerStats.Instance.AddDrinkPoints(1); // Add 1 food point to the player
                PlayerStats.Instance.DeductCurrency(currencyCost); // Deduct currency
                Debug.Log("Food point added. Current Food Points: " + PlayerStats.Instance.GetDrinkPoints());
                Debug.Log("Currency deducted. Remaining Currency: $" + PlayerStats.Instance.GetCurrency());
            }
            else
            {
                Debug.Log("Not enough currency! Current Currency: $" + PlayerStats.Instance.GetCurrency());
            }
        }
        else
        {
            Debug.LogError("PlayerStats instance not found!");
        }
    }
}