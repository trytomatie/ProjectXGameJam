using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerDolly : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private CinemachineTrackedDolly dolly;

    // Start is called before the first frame update
    void Start()
    {
        dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
       // dolly.m_Path.FindClosestPoint(dolly.FollowTarget.position,0,)
    }
}
