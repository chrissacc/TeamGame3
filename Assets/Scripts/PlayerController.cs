using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 tempAddForce;
    public float jumpAmount = 5f;
    public float speed = 1f;
    private Rigidbody RB;
    private Vector3 direction; //direction the player is moving this frame
    public Vector3 ExternalDecay; //How much the external velocity should decrease every second 
    public Vector3 externalVelocity; //How much velocity outside of movement should be affecting the player. Includes jumping
    public bool canJump;
    private bool clickedJump;
    private bool falling;
    private float prevYvel;
    private float StunAmount;
    private float StunDuration;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StunDuration > 0f)
        {
            StunDuration -= Time.deltaTime;
            if (StunDuration <= 0f) 
            {
                StunDuration = 0f;
                StunAmount = 0f;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            clickedJump = true;
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
        if (RB.velocity.y > -.1f && RB.velocity.y < .1f) 
        {
            if (prevYvel > -.05f && prevYvel < .05f) canJump = true;
            else canJump = false;
        }
        else canJump = false;
        Vector3 movementVelocity = direction.normalized * speed * (1 - StunAmount);
        Vector3 totalVelocity = movementVelocity + externalVelocity;
        RB.velocity = new Vector3(totalVelocity.x, RB.velocity.y, totalVelocity.z);
        VelocityDecay();
        if (clickedJump) 
        {
            clickedJump = false;
            if (canJump) RB.AddForce(new Vector3(0f, jumpAmount, 0f), ForceMode.Impulse);
        }
        prevYvel = RB.velocity.y;
    }

    public void addVelocity(Vector3 amount)
    {
        externalVelocity += amount;
    }

    private void VelocityDecay()
    {
        Vector3 pieceOfDecay = ExternalDecay;
        pieceOfDecay.x = pieceOfDecay.x / 30f;
        pieceOfDecay.z = pieceOfDecay.z / 30f;
        externalVelocity.x = Mathf.MoveTowards(externalVelocity.x, 0f, pieceOfDecay.x);
        externalVelocity.z = Mathf.MoveTowards(externalVelocity.z, 0f, pieceOfDecay.z);
    }
    private void OnCollisionEnter(Collision other) 
    {

    }
    public void StunPlayer(float length, float strength) //a strength of 1 means cannot move at all 
    {
        if (strength > StunAmount) StunAmount = strength;
        if (length > StunDuration) StunDuration = length;  
    }
}
