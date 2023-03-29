using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform waypoint;
    public Animator anim;
    public float aggroRadius = 3;
        // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(waypoint.position,transform.position) < aggroRadius)
        {
            agent.destination = waypoint.position;
        }
        else
        {
            agent.destination = transform.position;
        }

        if(agent.destination != transform.position && Vector3.Distance(agent.destination, transform.position) < 1)
        {
            anim.SetBool("attack", true);
        }
        else
        {
            anim.SetBool("attack", false);
        }

        Animate();
    }

    private void Animate()
    {
        anim.SetFloat("speed", agent.velocity.magnitude);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
