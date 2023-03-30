using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class PlayerMainScipt : MonoBehaviour
{
    //States
    [HideInInspector]
    public PlayerIdleState plIdle = new PlayerIdleState();
    [HideInInspector]
    public PlayerMoveState plMove = new PlayerMoveState();

    //layer
    public LayerMask groundLayer; // the layer(s) that the player can stand on

    //Public
    public PlayerBaseState currentState;

    public Transform camer;

    //Components
    [HideInInspector]
    public CharacterController plCharacterController;
    [HideInInspector]
    public Animator plAnimator;

    //Game Objects
    public Camera mainCam;
    public CinemachineFreeLook virtualCmCam;
    public GameObject playerMesh;

    //Helper GameObjects
    public GameObject rightHandIK;
    public GameObject leftHandIK;

    public GameObject sideLight;

    //GameplayStuff
    public float health;
    public float jumpHeight;
    public float speed;
    public float damage;
    public float playerHeight;

    //Private
    private float verticalInput;
    private float horizontalInput;

    private bool sideLightBool;

    // Start is called before the first frame update
    void Start()
    {
        sideLightBool = false;
        sideLight.SetActive(sideLightBool);
        // Get Components
        plCharacterController = GetComponent<CharacterController>();
        plAnimator = playerMesh.GetComponent<Animator>();

        //Start Statemashine
        currentState = plMove;
        currentState.EnterPlayerState(this);
    }

    // Update is called once per frame
    void Update()
    {
        CheckSideLight();

        //Update current state //Movement State // Target State // climbing state // interacting state // Carrying // Dragging
        currentState.UpdatePlayerState(this);
    }

    private void CheckSideLight()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sideLightBool = !sideLightBool;
            sideLight.SetActive(sideLightBool);
        }
    }

    public void SwitchMouseState(PlayerBaseState newPlayerState)
    {
        currentState.ExitPlayerState(this);
        currentState = newPlayerState;
        currentState.EnterPlayerState(this);
    }
}
