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
    [HideInInspector]
    public PlayerTargetState plTarget = new PlayerTargetState();
    [HideInInspector]
    public PlayerCarryState plCarry = new PlayerCarryState();
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
    public Transform handRight;
    public Transform holster;
    public GameObject weaponHand;
    public Transform carryHelperObject;

    //GameplayStuff
    public float health;
    public float jumpHeight;
    public float speed;
    public float damage;
    public float playerHeight;
    public float throwStrength;

    //Private
    private float verticalInput;
    private float horizontalInput;

    private bool sideLightBool;
    private bool weaponOut = false;

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
        CheckAttack();

        //Update current state //Movement State // Target State // climbing state // interacting state // Carrying // Dragging
        currentState.UpdatePlayerState(this);
        updateWeapon();
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
