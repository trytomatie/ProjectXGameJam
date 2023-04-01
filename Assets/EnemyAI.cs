using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State { Walking, Attacking };
    public State state = State.Walking;
    private NavMeshAgent agent;
    public Transform target;
    public Animator anim;
    public float attackDelay = 1;
    public float aggroRadius = 3;
    public float attackCooldown = 4;
    private float attackTimer;

    public StabilizerController stabilizer;
        // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerStabilizer").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position,transform.position) < aggroRadius)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = transform.position;
        }

        if(attackTimer < Time.time && agent.destination != transform.position && Vector3.Distance(agent.destination, transform.position) < 1 && !IsInvoking("Attack"))
        {
            attackTimer = Time.time + attackCooldown;
            state = State.Attacking;
            Invoke("Attack", attackDelay);
        }

        switch(state)
        {
            case State.Walking:
                if(!anim.GetNextAnimatorStateInfo(0).IsName("Attacking"))
                {
                    agent.speed = 2;
                }
                anim.SetBool("attack", false);
                break;
            case State.Attacking:
                agent.speed = 0;
                break;
        }

        Animate();
    }

    private void Animate()
    {
        if (stabilizer.ragdolling)
        {
            anim.SetFloat("speed", 0);
            return;
        }

        anim.SetFloat("speed", agent.velocity.magnitude);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }

    private void Attack()
    {
        if (!stabilizer.ragdolling)
        {
            anim.SetBool("attack", true);
        }
        state = State.Walking;
    }
}
