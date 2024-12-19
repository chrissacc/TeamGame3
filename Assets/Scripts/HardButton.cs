using UnityEngine;
using UnityEngine.SceneManagement;

public class HardButton : MonoBehaviour
{
    [SerializeField] private string sceneName = "HardLevel"; // Name of the scene to load

    // This function is called when the button is pressed
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set in the inspector!");
        }
    }
}