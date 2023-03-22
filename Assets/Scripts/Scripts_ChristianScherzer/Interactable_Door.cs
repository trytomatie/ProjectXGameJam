using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Door : Interactable
{
    public Animator anim;
    public bool animationCollision = false;

    private Collider coll;

    private void Start()
    {
        gameObject.isStatic = false;
        coll = GetComponent<Collider>();
    }
    public override void Interaction(GameObject source)
    {
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));
        if(Vector3.Dot(transform.TransformDirection(transform.forward), source.transform.position - transform.position) < 0)
        {
            anim.SetBool("front", false);
        }
        else
        {
            anim.SetBool("front", true);
        }
        if (animationCollision == false)
        {
            coll.isTrigger = true;
            Invoke("SetColliderSolid", 0.5f);
        }
    }

    public override void TriggerAnimation(GameObject source)
    {
        source.GetComponent<Animator>().SetTrigger("openDoor");
    }

    /// <summary>
    /// Sets the collider of the Door back to solid
    /// </summary>
    public void SetColliderSolid()
    {
        coll.isTrigger = false;
    }
}

