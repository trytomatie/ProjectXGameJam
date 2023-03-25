using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetScript : MonoBehaviour
{
    public GameObject lookAt;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 direction = lookAt.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
}
