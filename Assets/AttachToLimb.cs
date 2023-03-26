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
    public ConfigurableJoint[] limbJoints;
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
            if(target != null && target.tag =="Item")
            {
                isGrabbing = true;
                targetJoint = target.AddComponent<FixedJoint>();
                targetJoint.connectedBody = rb;
                targetJoint.breakForce = 9000;
                targetJoint.connectedMassScale = 0.7f;
                // targetJoint.massScale = 11111;
            }

            if (target != null && target.tag == "Grabable")
            {
                isGrabbing = true;
                targetJoint = gameObject.AddComponent<FixedJoint>();
                targetJoint.connectedBody = target.GetComponent<Rigidbody>();
                targetJoint.breakForce = 6000;
                targetJoint.connectedMassScale = 0.01f;
                targetJoint.massScale = 11111;
            }
        }
        if (Input.GetMouseButtonUp(mouseButton))
        {
            isGrabbing = false;
            if (target != null)
            {
                if (targetJoint != null)
                {
                    Destroy(targetJoint);
                }
                target = null;

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item" || collision.gameObject.tag == "Grabable")
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
