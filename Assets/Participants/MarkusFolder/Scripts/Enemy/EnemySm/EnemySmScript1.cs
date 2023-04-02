using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySmScript1 : MonoBehaviour
{
    GameObject player;
    public float viewDistance = 20;
    public LayerMask targetLayer;
    [HideInInspector]
    public NavMeshAgent navMeshEnemAgent;
    public float attackCoolDown = 3;
    public float damage = 3;
    private float countdown;
    //private bool attackRange;

    // Start is called before the first frame update
    private void Awake()
    {
        navMeshEnemAgent = GetComponent<NavMeshAgent>();
        countdown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForStuff(transform.position, 100);
        

    }

    void CheckForStuff(Vector3 center, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(center, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Player"))
            {

                // Do something when player is found
                Debug.Log("Player is in sphere!");
                player = colliders[i].gameObject;
                if (player.GetComponent<PlayerMainScipt>().lightIndex == 0)
                {
                    if (PlayerInView(player))
                    {
                        if (Vector3.Distance(player.transform.position, transform.position) > 1)

                            navMeshEnemAgent.SetDestination(player.transform.position + (transform.position - player.transform.position).normalized);

                        if (Vector3.Distance(player.transform.position, transform.position) < 1.5)
                        {
                            countdown = countdown + 1 * Time.deltaTime;
                            if (countdown > attackCoolDown)
                            {
                                countdown = 0;
                                player.GetComponent<PlayerMainScipt>().GetDamage(damage);
                            }
                        }

                    }

                }
                //if (player.GetComponent<PlayerMainScipt>().lightIndex > 0)
            }
            if (colliders[i].gameObject.CompareTag("LightSource"))
            {
                // Do something when player is found
                Debug.Log("LightSource in Sphere");

            }
        }

    }

    private bool PlayerInView(GameObject player)
    {
        Vector3 direction = player.transform.position - transform.position;
        if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(direction.x, direction.z)) < 110)
        {
            if (Physics.Raycast(transform.position, direction, viewDistance, targetLayer))
            {
                return true;
            }
        } 
        return false;
    }
}
