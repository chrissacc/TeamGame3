using UnityEngine;

public class WindmillPillar : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Axis of rotation
    public float rotationSpeed = 50f;        // Speed of rotation

    void Update()
    {
        // Rotate the windmill 
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}