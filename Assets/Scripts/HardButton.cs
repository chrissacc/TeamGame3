using UnityEngine;
using UnityEngine.SceneManagement;

public class HardButton : MonoBehaviour
{
    [SerializeField] private string sceneName = "HardLevel"; // scene name load

    // load scene when button pressed
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