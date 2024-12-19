using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Scenes
{
    public class CollisionGameOver : MonoBehaviour
    {
        [SerializeField]
        private Transform respawnPoint;

        [SerializeField]
        private List<FallingPlatform> fallingPlatforms;

        private int failCounter = 0;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                failCounter++;
                PlayerStats.Instance.UpdateFailCounter(failCounter);

                if (failCounter >= 3)
                {
                    PlayerStats.Instance.EndCourse();
                    SceneManager.LoadScene("TestMenu");
                }
                else
                {
                    collision.gameObject.transform.position = respawnPoint.position;

                    PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
                    if (playerController != null)
                    {
                        playerController.ResetPlayerState();
                    }

                    RespawnPlatforms();
                }
            }
        }

        private void RespawnPlatforms()
        {
            if (fallingPlatforms == null || fallingPlatforms.Count == 0)
            {
                Debug.LogError("No falling platforms assigned or found!");
                return;
            }

            foreach (FallingPlatform platform in fallingPlatforms)
            {
                if (platform != null)
                {
                    platform.RespawnPlatform();
                }
                else
                {
                    Debug.LogError("Platform reference is null!");
                }
            }
        }

        private void Start()
        {
            if (fallingPlatforms == null || fallingPlatforms.Count == 0)
            {
                fallingPlatforms = new List<FallingPlatform>(FindObjectsOfType<FallingPlatform>());
                Debug.Log($"Found {fallingPlatforms.Count} platforms in the scene.");
            }
        }
    }
}
