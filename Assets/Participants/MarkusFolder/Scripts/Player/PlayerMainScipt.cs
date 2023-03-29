using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMainScipt : MonoBehaviour
{
    //States
    [HideInInspector]
    public PlayerIdleState plIdle = new PlayerIdleState();
    [HideInInspector]
    public PlayerMoveState plMove = new PlayerMoveState();

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

    //GameplayStuff
    public float health;

    public float speed;
    public float damage;

    //Private
    private float verticalInput;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
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
        //get Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Get the camera's forward vector and flatten it
        Vector3 cameraForward = Vector3.Scale(camer.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (horizontalInput * camer.right + verticalInput * cameraForward).normalized;



        //Update current state //Movement State // Target State // climbing state // interacting state // Carrying // Dragging
        currentState.UpdatePlayerState(this);
    }

    public void SwitchMouseState(PlayerBaseState newPlayerState)
    {
        currentState.ExitPlayerState(this);
        currentState = newPlayerState;
        currentState.EnterPlayerState(this);
    }
}
