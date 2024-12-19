using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    private PlayerStats playerStats;    // Reference to the PlayerStats script
    private float currentSpeed;         // Player's current speed (updated dynamically)
    private float reducedSpeed;         // Speed while on StickyWall
    private bool isOnStickyWall = false; // Is the player on a sticky wall?
    public Vector3 tempAddForce;
    public float jumpAmount = 5f;
    public float speed = 1f;
    private Rigidbody RB;
    private Vector3 direction; // Direction the player is moving this frame
    public Vector3 ExternalDecay; // How much the external velocity should decrease every second 
    public Vector3 externalVelocity; // How much velocity outside of movement should be affecting the player. Includes jumping
    public bool canJump;
    private bool clickedJump;
    private bool falling;
    private float prevYvel;
    private float StunAmount;
    private float StunDuration;
    private float CurrentRot;
    public GameObject body;
    public GameObject legs;
    private KillPlayer KP;
    public int DeathsonLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        KP = FindObjectOfType<KillPlayer>();
        playerStats = PlayerStats.Instance;
        RB = GetComponent<Rigidbody>();

        // Set the initial speed to currentSpeed from PlayerStats
        currentSpeed = playerStats.currentSpeed;

        reducedSpeed = currentSpeed * 0.5f; // Example slow-down factor
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
        if (direction.x > 0f && direction.z == 0f) CurrentRot = 90f;
        else if (direction.x < 0f && direction.z == 0f) CurrentRot = -90f;
        else if (direction.z > 0f && direction.x == 0f) CurrentRot = 0f;
        else if (direction.z < 0f && direction.x == 0f) CurrentRot = 180f;

        else if (direction.x > 0f && direction.z > 0f) CurrentRot = 45f;
        else if (direction.x < 0f && direction.z > 0f) CurrentRot = -45f;
        else if (direction.x > 0f && direction.z < 0f) CurrentRot = 135f;
        else if (direction.x < 0f && direction.z < 0f) CurrentRot = 225f;

        if (direction.magnitude == 0f) CurrentRot = 0f;
    }

    private void FixedUpdate()
    {

        // Determine if player can jump (based on their Y velocity)
        if (RB.velocity.y > -.1f && RB.velocity.y < .1f)
        {
            if (prevYvel > -.05f && prevYvel < .05f) canJump = true;
            else canJump = false;
        }
        else canJump = false;

        // Use the updated speed from PlayerStats when moving
        Vector3 movementVelocity = direction.normalized * playerStats.currentSpeed * (1 - StunAmount);
        Vector3 totalVelocity = movementVelocity + externalVelocity;
        RB.velocity = new Vector3(totalVelocity.x, RB.velocity.y, totalVelocity.z);

        VelocityDecay();

        if (clickedJump)
        {
            clickedJump = false;
            if (canJump) RB.AddForce(new Vector3(0f, playerStats.currentJumpHeight, 0f), ForceMode.Impulse);
        }

        prevYvel = RB.velocity.y;
        body.transform.localEulerAngles = new Vector3(0f, CurrentRot, 0f);
        legs.transform.localEulerAngles = new Vector3(0f, CurrentRot, 0f);
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
        if (other.gameObject.CompareTag("RestartObject"))
        {
            RestartLevel();
        }
        // Apply knockback when colliding with anything that can apply knockback
        if (other.relativeVelocity.magnitude > 1f) // If the collision is strong enough
        {
            ApplyKnockback(other);
        }
    }

    public void StunPlayer(float length, float strength)
    {
        if (strength > StunAmount) StunAmount = strength;
        if (length > StunDuration) StunDuration = length;
    }

    public void OnStickyWallEnter()
    {
        isOnStickyWall = true;
        currentSpeed = reducedSpeed; // Set speed to reduced value
    }

    // Method to restore normal speed when exiting StickyWall
    public void OnStickyWallExit()
    {
        isOnStickyWall = false;
        currentSpeed = playerStats.currentSpeed; // Restore original speed from PlayerStats
    }

    private void ApplyKnockback(Collision other)
    {
        // Apply knockback with a strength that is based on player's knockback stat
        Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
        float knockbackStrength = playerStats.currentKnockback; // Dynamic knockback strength from PlayerStats

        // Apply the knockback force, scaled by the player's knockback stat
        RB.AddForce(knockbackDirection * knockbackStrength, ForceMode.Impulse);
    }

    // Method to reset the player's state when respawning
    public void ResetPlayerState()
    {
        // Reset velocity to zero to stop all movement
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;

        // Reset any movement-related states
        externalVelocity = Vector3.zero;
        direction = Vector3.zero;
        isOnStickyWall = false;

        // Reset stun
        StunAmount = 0f;
        StunDuration = 0f;

        // Restore the original speed
        currentSpeed = playerStats.currentSpeed;
    }

    public void RestartLevel()
    {
        DeathsonLevel++;
        externalVelocity = new Vector3();
        RB.velocity = new Vector3();
        KP.RespawnPlayer();

    }
}
