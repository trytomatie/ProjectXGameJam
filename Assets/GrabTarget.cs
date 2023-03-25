using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GrabTarget : MonoBehaviour
{
    private Vector2 startPos;

    public Transform mainCamera;

    public Transform rigTarget;
    public Transform rotationTarget;
    public Vector4 remapX;
    public Vector4 remapY;
    public Rig rig;
    public ChainIKConstraint rigLeft;
    public ChainIKConstraint rigRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            transform.localEulerAngles = Vector3.zero;
        }

        if(Input.GetMouseButton(0))
        {
            rigLeft.weight = 1;
        }
        else
        {
            rigLeft.weight = 0;
        }

        if (Input.GetMouseButton(1))
        {
            rigRight.weight = 1;
        }
        else
        {
            rigRight.weight = 0;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            transform.localEulerAngles = new Vector3(ExtensionMethods.Remap(mainCamera.eulerAngles.x, remapX.x,remapX.y, remapX.z, remapX.w),
                ExtensionMethods.Remap(mainCamera.eulerAngles.y, remapY.x, remapY.y, remapY.z, remapY.w), 0);
            rigTarget.position = rotationTarget.position;
        }

    }
}
