using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.SceneManagement;

public class PlayerMainScipt : MonoBehaviour
{
    //States
    [HideInInspector]
    public PlayerIdleState plIdle = new PlayerIdleState();
    [HideInInspector]
    public PlayerMoveState plMove = new PlayerMoveState();
    [HideInInspector]
    public PlayerTargetState plTarget = new PlayerTargetState();
    [HideInInspector]
    public PlayerCarryState plCarry = new PlayerCarryState();
    [HideInInspector]
    public PlayerDraggingState plDragg = new PlayerDraggingState();
    [HideInInspector]
    public PlayerClimbingState plClimb = new PlayerClimbingState();
    
    

    //layer
    public LayerMask groundLayer; // the layer(s) that the player can stand on
    public LayerMask plLayerMask;
    public LayerMask targetLayer;

    //Public
    public PlayerBaseState currentState;

    public Transform camer;

    public int lightIndex;

    //Components
    [HideInInspector]
    public CharacterController plCharacterController;

    [HideInInspector]
    public Animator plAnimator;

    //Game Objects
    public Camera mainCam;
    public CinemachineFreeLook virtualCmCam;
    public GameObject playerMesh;
    public GameObject targetObject;

    //Helper GameObjects
    public GameObject rightHandIK;
    public GameObject leftHandIK;

    public GameObject sideLight;
    public Transform handRight;
    public Transform holster;
    public GameObject weaponHand;
    public Transform carryHelperObject;
    public GameObject dragHelper;
    public GameObject dragItem;
    public GameObject climbWall;
    //other
    public bool weaponOut = false;


    //GameplayStuff
    public float health;
    public float jumpHeight;
    public float speed;
    public float damage;
    public float playerHeight;
    public float throwStrength;
    public float targetMaxDistance=10;


    //Private
    private float verticalInput;
    private float horizontalInput;

    private bool sideLightBool;

    // Start is called before the first frame update
    void Start()
    {
        virtualCmCam.m_XAxis.m_InputAxisName = "Mouse X";
        virtualCmCam.m_YAxis.m_InputAxisName = "Mouse Y";

        sideLightBool = false;
        sideLight.SetActive(sideLightBool);
        // Get Components
        plCharacterController = GetComponent<CharacterController>();
        plAnimator = playerMesh.GetComponent<Animator>();

        //Start Statemashine
        currentState = plMove;
        currentState.EnterPlayerState(this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckPlayerInLight();
        CheckSideLight();
        CheckAttack();

        //Update current state //Movement State // Target State // climbing state // interacting state // Carrying // Dragging
        currentState.UpdatePlayerState(this);
        updateWeapon();
        CheckOutOfWorld();
    }

    private void CheckOutOfWorld()
    {
        if (transform.position.y < -27)
        {
            string sceneName = SceneManager.GetActiveScene().name;

            // Reload the scene with the given name
            SceneManager.LoadScene(sceneName);
        }
    }

    private void updateWeapon()
    {
        if (weaponOut)
        {
            weaponHand.transform.position = handRight.transform.position;
            weaponHand.transform.rotation = handRight.transform.rotation;
        }
        else
        {
            weaponHand.transform.position = holster.transform.position;
            weaponHand.transform.rotation = holster.transform.rotation;
        }
    }

    private void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentState != plMove || currentState != plTarget)
            {
                SwitchPlayerState(plMove);
            }
            weaponOut = true;
            plAnimator.SetTrigger("Attack");
        }
    }

    private void CheckSideLight()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sideLightBool = !sideLightBool;
            sideLight.SetActive(sideLightBool);
        }
    }

    public void SwitchPlayerState(PlayerBaseState newPlayerState)
    {
        currentState.ExitPlayerState(this);
        currentState = newPlayerState;
        currentState.EnterPlayerState(this);
    }

    public void AttackStart()
    {
        //wenn eine Waffe in der Hand ist dann 
        if (weaponHand.transform.GetChild(0).GetComponent<WeaponScript>()!=null)
        {
            weaponHand.transform.GetChild(0).GetComponent<WeaponScript>().weaponCollider.enabled = true;
            //Debug.Log("GotChild" + weaponHand.transform.GetChild(0));
            /*if (weaponHand.transform.GetChild(0).GetComponent<WeaponScript>().weaponCollider.enabled == true)
            {
                Debug.Log("EnabledCollider");
            }*/
        }

    }

    public void AttackEnd()
    {
        if (weaponHand.transform.GetChild(0).GetComponent<WeaponScript>() != null)
        {
            weaponHand.transform.GetChild(0).GetComponent<WeaponScript>().weaponCollider.enabled = false;
            //Debug.Log("GotChild" + weaponHand.transform.GetChild(0));
        }
    }

    public void DealDamage(Collider other, float damage, bool lightDamage)
    {
        if (other.gameObject.GetComponent<EnemyScript>() != null)
        {
            other.gameObject.GetComponent<EnemyScript>().GetDamage(damage, lightDamage);
            Debug.Log("MainPlayer DealDamage Throw");
            if (other.gameObject.GetComponent<Rigidbody>())
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*20 ,ForceMode.Force); //functioniert nicht vlt wann anders
            }
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Get the name of the current scene
            string sceneName = SceneManager.GetActiveScene().name;

            // Reload the scene with the given name
            SceneManager.LoadScene(sceneName);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        currentState.PlayerTriggerEnter(this, other);
    }
    private void OnTriggerStay(Collider other)
    {
        currentState.PlayerTriggerStay(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.PlayerTriggerExit(this, other);
    }
}
