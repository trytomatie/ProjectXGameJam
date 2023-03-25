using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class CopyLimb : MonoBehaviour
{
    public Transform targetLimb;
    private ConfigurableJoint configurableJoint;

    private Quaternion targetInitialRotation;

    public Vector3 targetRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        targetInitialRotation = targetLimb.transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        configurableJoint.targetRotation = CopyRotation();
    }

    private Quaternion CopyRotation()
    {
        return Quaternion.Inverse(this.targetLimb.localRotation * Quaternion.Euler(targetRotationOffset)) * this.targetInitialRotation;
    }
}
