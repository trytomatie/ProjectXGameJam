using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleState_IsLooking", menuName = "States/SimpleState_IsLooking", order =1)]
public class SimpleState_IsLooking : SimpleState
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
        hc.targetPosition = Vector3.Lerp(hc.lookAtTarget.transform.position, hc.mainCamera.transform.forward * 3 + hc.lookStartPosition.transform.transform.position, Time.deltaTime * 5);
        hc.lookAtTarget.transform.position = hc.targetPosition;
    }

    public override string Transition()
    {
        if (Vector3.Angle(hc.mainCamera.transform.forward, hc.transform.forward) > hc.angleThreshold)
        {
            return "notLooking";
        }
        return "isLooking";
    }

    public override void ExitState()
    {

    }

}
