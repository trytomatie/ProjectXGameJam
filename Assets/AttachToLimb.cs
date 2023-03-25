using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToLimb : MonoBehaviour
{
    public int mouseButton;
    public GameObject target;
    private Rigidbody rb;
    private FixedJoint targetJoint;
    public bool isGrabbing;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(mouseButton) && !isGrabbing)
        {
            if(target != null)
            {
                isGrabbing = true;
                targetJoint = target.AddComponent<FixedJoint>();
                targetJoint.connectedBody = rb;
                targetJoint.breakForce = 9000;
            }
        }
        if (Input.GetMouseButtonUp(mouseButton))
        {
            isGrabbing = false;
            if (target != null)
            {
                if (targetJoint != null)
                {
                    targetJoint.breakForce = 100;
                }
                target = null;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            target = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (target == collision.gameObject && !isGrabbing)
        {
            target = null;
        }
    }
}
