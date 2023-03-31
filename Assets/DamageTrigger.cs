using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public StabilizerController stabilizer;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") && collision.gameObject.tag == "Limb")
        {
            CopyLimb targetLimb = collision.gameObject.GetComponent<CopyLimb>();
            if (targetLimb != null && targetLimb.stabilizer != stabilizer)
            {
                if (!targetLimb.stabilizer.ragdolling)
                {
                    targetLimb.stabilizer.force = (transform.position - collision.ClosestPointOnBounds(transform.position)).normalized * 333;
                    targetLimb.stabilizer.AddForceToStabilizer();
                    StatusManager targetStatus = targetLimb.stabilizer.controller.GetComponent<StatusManager>();
                    targetStatus.Hp -= 1;

                }
            }
        }
    }
}
