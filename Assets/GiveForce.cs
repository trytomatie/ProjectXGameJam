using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveForce : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(force);
    }
}
