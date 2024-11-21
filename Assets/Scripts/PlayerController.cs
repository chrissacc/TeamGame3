using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 tempAddForce;
    public float speed = 1f;
    private Rigidbody RB;
    private Vector3 direction; //direction the player is moving this frame
    public Vector3 ExternalDecay; //How much the external velocity should decrease every second 
    public Vector3 externalVelocity; //How much velocity outside of movement should be affecting the player. Includes jumping
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            addVelocity(tempAddForce);
        }
        direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction.z += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.z += -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x += -1f;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movementVelocity = direction.normalized * speed;
        Vector3 totalVelocity = movementVelocity + externalVelocity;
        RB.velocity = new Vector3(totalVelocity.x, RB.velocity.y, totalVelocity.z);
        VelocityDecay();
    }

    public void addVelocity(Vector3 amount)
    {
        externalVelocity += amount;
    }

    private void VelocityDecay()
    {
        Vector3 pieceOfDecay = ExternalDecay;
        pieceOfDecay.x = pieceOfDecay.x / 30f;
        pieceOfDecay.x = pieceOfDecay.y / 30f;
        pieceOfDecay.x = pieceOfDecay.z / 30f;
        
        externalVelocity.x = Mathf.MoveTowards(externalVelocity.x, 0f, pieceOfDecay.x);
        externalVelocity.z = Mathf.MoveTowards(externalVelocity.x, 0f, pieceOfDecay.z);
        //externalVelocity.y = Mathf.MoveTowards(externalVelocity.x, 0f, ExternalDecay.y);

    }
}
