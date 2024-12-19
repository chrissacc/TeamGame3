using UnityEngine;

public class LaserKnockback : MonoBehaviour
{
    public float knockbackForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Calculate knockback direction
                Vector3 direction = (collision.gameObject.transform.position - transform.position).normalized;

                // Apply knockback using the PlayerController's addVelocity method
                playerController.addVelocity(direction * knockbackForce);
            }

            // Destroy the laser after applying knockback
            Destroy(gameObject);
        }
    }
}
