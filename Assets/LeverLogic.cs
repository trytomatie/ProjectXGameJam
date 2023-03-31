using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverLogic : MonoBehaviour
{
    public UnityEvent leverOn;
    public UnityEvent leverOff;

    public bool isOn = false;

    public Transform leverModel;

    private void Start()
    {
        TriggerInteraction(isOn);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Weapon")
        {
            isOn = !isOn;
            TriggerInteraction(isOn);
        }
    }

    public void TriggerInteraction(bool value)
    {
        if(isOn)
        {
            leverModel.transform.localEulerAngles = new Vector3(-40, 0, 0);
            leverOn.Invoke();
        }
        else
        {
            leverModel.transform.localEulerAngles = new Vector3(40, 0, 0);
            leverOff.Invoke();
        }
    }
}
