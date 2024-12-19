using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private Restart RS;
    public StartPointScript SPS;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        RS = FindObjectOfType<Restart>();
        Player = FindObjectOfType<PlayerController>().gameObject;
        SPS = FindObjectOfType<StartPointScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        Player.transform.position = SPS.StartPoint;
    }
}
