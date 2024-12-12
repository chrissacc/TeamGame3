using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PistonScript : MonoBehaviour
{
    public Vector3 StartPosition;
    public Vector3 ExtendedPosition;
    public bool AutoActivate = true;
    public float cooldownTime = 4f;
    private float cooldownTimer;
    public float resetTime = 4f;
    private float resetTimer;
    public float extendTime = 1f;
    private float extendTimer;
    public bool IsActive;
    public bool ReadyToGo = true;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (resetTimer <= 0)
            {
                FinishReset();
            }   
        }
        if (extendTimer > 0f)
        {
            if (extendTimer <= 0)
            {

            }
        }
        if (IsActive)
        {
            Vector3 newPos = transform.position;
            newPos = Vector3.MoveTowards(newPos, ExtendedPosition, Time.fixedDeltaTime/extendTime);
            transform.position = newPos;
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
        newPos = Vector3.MoveTowards(newPos, StartPosition, resetTime/timePassed);
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
    public void ExtendMove()
    {

    }
    public void FinishExtend()
    {
        extendTimer = 0f;
        BeginReset();
    }
}
