using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableScript : MonoBehaviour
{
    public Vector3 movement;
    public Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        movement = Vector3.zero;
        lastPosition = transform.position;
    }

    private void Update()
    {
        // Calculates velocity
        if(lastPosition != transform.position)
        {
            movement = transform.position - lastPosition;
            lastPosition = transform.position;
        }
    }
}
