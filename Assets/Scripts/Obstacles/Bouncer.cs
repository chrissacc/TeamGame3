using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceForce = 10f; // Force applied to the player when they bounce

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the PlayerStats or PlayerMovement script
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            // Access the player's Rigidbody
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // Apply an upward force to bounce the player
                Vector3 bounceDirection = Vector3.up;
                playerRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

                // Optional: Debug message
                Debug.Log("Player bounced with force: " + bounceForce);
            }
        }
    }
}