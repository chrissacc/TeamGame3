using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public string sceneToLoad;  // Expose scene name in Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!string.IsNullOrEmpty(sceneToLoad))  // Check if scene name is set
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("Scene name is not set in Restart script!");
            }
        }
    }
}