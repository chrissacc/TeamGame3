using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonScript : MonoBehaviour
{
    public float StunLength = 2f;
    public float StunStrength = .9f; //amount of player movement the stun blocks, ranges 0-1
    private PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
        PC = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PC.StunPlayer(StunLength, StunStrength);
        }
    }
}
