using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public string sceneToLoad;  // scene name in inspect

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!string.IsNullOrEmpty(sceneToLoad))  // Check if scene name set
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