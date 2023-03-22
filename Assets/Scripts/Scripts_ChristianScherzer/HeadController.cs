using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : SimpleStateMachine
{
    public GameObject lookAtTarget;
    public GameObject lookStartPosition;
    public float angleThreshold = 90;
    public Camera mainCamera;
    public Vector3 targetPosition;
    public enum State {IsLooking,NotLooking};

    public State state = State.IsLooking;
    private new void Start()
    {
        base.Start();
        mainCamera = Camera.main;
    }

    public new void Update()
    {
        base.Update();
    }
}

