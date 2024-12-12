using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PistonScript : MonoBehaviour
{
    private PlayerController PC;
    public Vector3 StartPosition;
    public Vector3 ExtendedPosition;
    public bool AutoActivate = true;
    public float cooldownTime = 4f;
    private float cooldownTimer;
    public float resetTime = 4f;
    private float resetTimer;
    public float extendTime = 1f;
    private float extendTimer;
    private bool IsActive;
    public bool ReadyToGo = true;
    public float LaunchAmount = 1f;
    // Start is called before the first frame update
    void Start()
    {
        PC = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ReadyToGo && AutoActivate)
        {
            cooldownTimer -= Time.fixedDeltaTime;
            if (cooldownTimer <= 0f)
            {
                Activate();
            }
        }
        if (resetTimer > 0f)
        {
            resetTimer -= Time.fixedDeltaTime;
            if (resetTimer <= 0f)
            {
                FinishReset();
            }
            else 
            {
                ResetMove(Time.fixedDeltaTime);
            }   
        }
        if (extendTimer > 0f)
        {
            extendTimer -= Time.fixedDeltaTime;
            if (extendTimer <= 0f)
            {
                FinishExtend();
            }
            else ExtendMove(Time.fixedDeltaTime);
        }
    }
    public void Activate()
    {
        ReadyToGo = false;
        cooldownTimer = cooldownTime;
        IsActive = true;
        BeginExtend();
    }
    public void BeginReset()
    {
        resetTimer = resetTime;
    }
    public void ResetMove(float timePassed)
    {
        Vector3 newPos = transform.position;
        newPos = Vector3.Lerp(newPos, StartPosition, timePassed/resetTimer);
        transform.position = newPos;
    }
    public void FinishReset()
    {
        ReadyToGo = true;
        resetTimer = 0f;
        IsActive = false;
    }
    public void BeginExtend()
    {
        extendTimer = extendTime;
    }
    public void ExtendMove(float timePassed)
    {
        Vector3 newPos = transform.position;
        newPos = Vector3.Lerp(newPos, ExtendedPosition, timePassed/extendTimer);
        transform.position = newPos;
    }
    public void FinishExtend()
    {
        extendTimer = 0f;
        BeginReset();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("test2");
            if (IsActive)
            {
                PC.addVelocity(-transform.forward * LaunchAmount);
            }
        }
    }
}
