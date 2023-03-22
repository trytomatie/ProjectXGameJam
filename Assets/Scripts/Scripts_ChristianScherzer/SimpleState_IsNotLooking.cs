using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleState_IsNotLooking", menuName = "States/SimpleState_IsNotLooking", order = 2)]
public class SimpleState_IsNotLooking : SimpleState
{
    private  HeadController hc;
    public override void EnterState(GameObject source)
    {
        if(hc == null)
        {
            hc = source.GetComponent<HeadController>();
        }
    }

    public override void UpdateState()
    {
        hc.targetPosition = Vector3.Lerp(hc.lookAtTarget.transform.position, hc.transform.forward * 3 + hc.lookStartPosition.transform.transform.position, Time.deltaTime * 5);
        hc.lookAtTarget.transform.position = hc.targetPosition;
    }

    public override string Transition()
    {
        if (Vector3.Angle(hc.mainCamera.transform.forward, hc.transform.forward) <= hc.angleThreshold)
        {
            return "isLooking";
        }
        return "notLooking";
    }

    public override void ExitState()
    {

    }

}
