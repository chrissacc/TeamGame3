using UnityEngine;
using UnityEngine.SceneManagement; // For scene transitions

public class EndCourse : MonoBehaviour
{
    [SerializeField] private string pointSelectionSceneName = "PointSelection"; // Scene to transition to
    [SerializeField] private int currencyReward = 100; // Currency to be rewarded at the end of the course

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            PlayerStats.Instance.EndCourse(); // Trigger the EndCourse logic
            RewardPlayerCurrency(); // Add currency reward to the player
            LoadPointSelectionScene(); // Transition to the point selection scene
        }
    }

    // Adds the reward currency to the player
    private void RewardPlayerCurrency()
    {
        PlayerStats.Instance.AddCurrency(currencyReward); // Add the specified currency amount
        Debug.Log("Currency rewarded: $" + currencyReward);
    }

    // Load the point selection scene
    private void LoadPointSelectionScene()
    {
        SceneManager.LoadScene(pointSelectionSceneName);
    }
}