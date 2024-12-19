using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointScript : MonoBehaviour
{
    public Vector3 StartPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
