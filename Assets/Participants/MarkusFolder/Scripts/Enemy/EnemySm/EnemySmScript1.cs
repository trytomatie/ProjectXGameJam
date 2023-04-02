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
        CheckForStuff(transform.position, 8);
        

    }

    void CheckForStuff(Vector3 center, float radius)
    {
        /*Vector3 lightDestination = null;
        Vector3 playerDestination = null;*/


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

                        //playerDestination = player.transform.position;
                        navMeshEnemAgent.SetDestination(player.transform.position );

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
            if (colliders[i].gameObject.GetComponent<LightSource>()) //maybe one day
            {
                
                // Do something when lightsoucrce is found
                if (Vector3.Distance(colliders[i].gameObject.transform.position, transform.position) < colliders[i].gameObject.GetComponent<LightSource>().influenceRadius)
                {
                    Debug.Log("LightSource in Sphere");
                    Vector3 direction = (transform.position - colliders[i].gameObject.transform.position).normalized;
                    Vector3 destination = transform.position + new Vector3(direction.x, 0, direction.z);
                    Debug.Log(destination + " enemy heading there");
                    navMeshEnemAgent.SetDestination(destination);
                }
                
                //lightDestination=destination;
            }
        }
        /*
        if (lightDestination != null)
        {
            navMeshEnemAgent.SetDestination(lightDestination);
        } else if (player!= null)
        {
            navMeshEnemAgent.SetDestination(playerDestination);
        }
        */
    }

    private bool PlayerInView(GameObject player)
    {
        RaycastHit hit;
        Vector3 direction = (player.transform.position - (transform.position + Vector3.up)).normalized;
        if (Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(direction.x, direction.z)) < 110)
        {
            if (Physics.Raycast(transform.position+Vector3.up, direction, out hit, viewDistance, targetLayer))
            {
                
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("I can see you");
                    Debug.DrawLine(transform.position + Vector3.up, hit.point, Color.green);
                    return true;
                }
                Debug.DrawLine(transform.position + Vector3.up, hit.point, Color.yellow);
            }
        }
        Debug.DrawRay(transform.position + Vector3.up, direction * viewDistance, Color.white);
        return false;
    }
}
