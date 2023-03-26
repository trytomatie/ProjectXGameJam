using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SchalterTrigger : MonoBehaviour
{
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > 1)
            enterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (Time.time > 1)
            exitEvent.Invoke();
    }
}
