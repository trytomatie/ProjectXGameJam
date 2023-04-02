using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public float influenceRadius;
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
        //Debug.Log(other + " has entered the trigger Enter LightSource");
        if (other.gameObject.GetComponent<InteractWithLight>())
        {
            other.gameObject.GetComponent<InteractWithLight>().EnterLight();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMainScipt>().lightIndex++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InteractWithLight>())
        {
            other.gameObject.GetComponent<InteractWithLight>().LooseLight();

        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMainScipt>().lightIndex--;
        }
    }
}
