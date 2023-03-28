using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform waypoint;
    public Animator anim;
        // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = waypoint.position;
        Animate();
    }

    private void Animate()
    {
        anim.SetFloat("speed", agent.velocity.magnitude);
    }
}
