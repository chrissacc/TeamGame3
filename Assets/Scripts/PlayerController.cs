using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody RB;

    private Vector3 movementVelocity;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVelocity = -movementVelocity;
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction.z = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1f;
        }
        movementVelocity = direction * speed;
        newVelocity += movementVelocity;
        RB.velocity += newVelocity;
    }
}
