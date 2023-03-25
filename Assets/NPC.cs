using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;
    public float speedAnimMultiplier = 10;
    private Rigidbody rb;
    public Transform waypoint;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    { 
        agent.destination = waypoint.transform.position;
        anim.SetFloat("speed", agent.desiredVelocity.magnitude * speedAnimMultiplier);
        rb.velocity = agent.desiredVelocity;
        agent.velocity = Vector3.zero;
    }
}
