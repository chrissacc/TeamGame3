using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCourse : MonoBehaviour
{
    [SerializeField] private string pointSelectionSceneName = "PointSelection";
    [SerializeField] private int currencyReward = 100;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter detected: {other.name}");
        
        if (other.CompareTag("Player"))
        {
            if (PlayerStats.Instance == null)
            {
                Debug.LogError("PlayerStats.Instance is null. Ensure PlayerStats script is attached and initialized.");
                return;
            }
            
            Debug.Log("Player reached goal. Rewarding currency...");
            PlayerStats.Instance.EndCourse();
            RewardPlayerCurrency();
            LoadPointSelectionScene();
        }
    }

    private void RewardPlayerCurrency()
    {
        Debug.Log($"Rewarding player: ${currencyReward}");
        PlayerStats.Instance.AddCurrency(currencyReward);
    }

    private void LoadPointSelectionScene()
    {
        Debug.Log($"Loading scene: {pointSelectionSceneName}");
        SceneManager.LoadScene(pointSelectionSceneName);
    }
}