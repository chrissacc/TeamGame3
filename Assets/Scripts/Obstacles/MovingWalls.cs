using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right; // Direction objects are pushed
    public float force = 2f;                      // Speed of the push

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = moveDirection.normalized * force;
        }
    }
}