using UnityEngine;

public class WindmillPillar : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Axis of rotation (e.g., Vector3.up for Y-axis)
    public float rotationSpeed = 50f;        // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the windmill pillar around the specified axis
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}