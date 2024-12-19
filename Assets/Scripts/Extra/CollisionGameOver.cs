using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class CollisionGameOver : MonoBehaviour
    {
        [SerializeField]
        private Transform respawnPoint; // Set this in the Unity editor.

        private int failCounter = 0; // Counter to track player collisions.

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                failCounter++;
                PlayerStats.Instance.UpdateFailCounter(failCounter); // Update failCounter in PlayerStats.

                if (failCounter >= 3)
                {
                    // Call EndCourse before loading the next scene.
                    PlayerStats.Instance.EndCourse();

                    // Load the next scene when failCounter reaches 3.
                    SceneManager.LoadScene("TestMenu");
                }
                else
                {
                    // Respawn the player at the respawn point.
                    collision.gameObject.transform.position = respawnPoint.position;

                    // Get the PlayerController component to reset movement and velocity.
                    PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

                    if (playerController != null)
                    {
                        // Reset player's velocity and movement-related values via the PlayerController script
                        playerController.ResetPlayerState();
                    }
                }
            }
        }
    }
}