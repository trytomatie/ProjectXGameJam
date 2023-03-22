using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHandler : State
{
    private bool isThrowing = false;
    public GameObject cheese;
    public Transform anchorPoint;

    private GameObject currentThrownItem;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void EnterState(GameObject source)
    {
        anim.SetTrigger("throw");
        isThrowing = true;
        currentThrownItem = Instantiate(cheese, anchorPoint.position, anchorPoint.rotation, anchorPoint);
        currentThrownItem.GetComponent<Rigidbody>().isKinematic = true;
        currentThrownItem.GetComponent<Collider>().enabled = false;
    }
    public void TriggerThrowEvent(string cmd)
    {
        if(cmd == "throwItem")
        {
            currentThrownItem.transform.parent = null;
            currentThrownItem.GetComponent<Rigidbody>().isKinematic = false;
            currentThrownItem.GetComponent<Rigidbody>().velocity = transform.forward * 3;
            currentThrownItem.GetComponent<Collider>().enabled = true;
        }

        if(cmd == "throwComplete")
        {
            anim.SetTrigger("throwComplete");
            isThrowing = false;
        }

    } 

    public override StateName Transition(GameObject source)
    {
        if(!isThrowing)
        {
            return State.StateName.Controlling;
        }
        return stateName;
    }

}
