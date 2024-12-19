using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyButton : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene"; // Name of scene load

    // call when button pressed
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