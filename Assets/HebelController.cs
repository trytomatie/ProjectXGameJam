using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HebelController : MonoBehaviour
{
    public UnityEvent right;
    public UnityEvent left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.eulerAngles.z > 20)
        {
            right.Invoke();
        }

        if (transform.eulerAngles.z < -20)
        {
            left.Invoke();
        }
    }
}
