using UnityEngine;

public class StickyWall : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // Factor to reduce player speed (0.5 = half speed)

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player != null)
        {
            // Call method to reduce player's speed when colliding with sticky wall
            player.OnStickyWallEnter();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player != null)
        {
            // Call method to restore player's speed when exiting collision with sticky wall
            player.OnStickyWallExit();
        }
    }
}