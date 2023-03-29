using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateController : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> entitiesOnPressurePlate = new List<GameObject>();

    public UnityEvent pressurePlateOn;
    public UnityEvent pressurePlateOff;
    private void OnTriggerEnter(Collider collision)
    {
        if(!entitiesOnPressurePlate.Contains(collision.gameObject))
        {
            print("test");
            if (entitiesOnPressurePlate.Count == 0)
            {
                pressurePlateOn.Invoke();
            }
            entitiesOnPressurePlate.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        print("test2");
        if (entitiesOnPressurePlate.Contains(collision.gameObject))
        {
            entitiesOnPressurePlate.Remove(collision.gameObject);
        }

        if(entitiesOnPressurePlate.Count == 0)
        {
            pressurePlateOff.Invoke();
        }
    }
}
