using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryState : PlayerBaseState
{
    GameObject carriedItem;
    private float horizontalInput;
    private float verticalInput;
    float turnSmoothVelocity = 3;
    float turnSmoothTime = 0.1f;
    //float gravityValue = -9.81f;

    bool groundedPlayer;
    float sprintMultiply;

    public override void EnterPlayerState(PlayerMainScipt player)
    {
        carriedItem = player.carryHelperObject.transform.GetChild(0).gameObject;
        carriedItem.transform.position = player.carryHelperObject.gameObject.transform.position;
        carriedItem.transform.rotation = player.carryHelperObject.gameObject.transform.rotation;
        carriedItem.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log(carriedItem);
        player.weaponOut = false;
    }

    public override void ExitPlayerState(PlayerMainScipt player)
    {
        if (player.carryHelperObject.transform.childCount > 0)
        {
            Debug.Log(carriedItem);
            carriedItem.GetComponent<Rigidbody>().useGravity = true;
            carriedItem.transform.SetParent(null);
        }
    }

    public override void PlayerTriggerEnter(PlayerMainScipt player, Collider other)
    {
    }

    public override void PlayerTriggerExit(PlayerMainScipt player, Collider other)
    {
    }

    public override void PlayerTriggerStay(PlayerMainScipt player, Collider other)
    {
    }

    public override void UpdatePlayerState(PlayerMainScipt player)
    {
        CheckThrow(player);


        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Get the camera's forward vector and flatten it
        Vector3 cameraForward = Vector3.Scale(player.camer.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (horizontalInput * player.camer.right + verticalInput * cameraForward).normalized;

        player.plCharacterController.Move(direction * (player.speed / 1.3f) * Time.deltaTime);
        //noise = 3;



        //rotate with camera angle
        float targetAngle = player.camer.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        player.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        //Animation
        player.plAnimator.SetFloat("MoveHorizontal", horizontalInput * player.speed);
        player.plAnimator.SetFloat("MoveVertical", verticalInput * player.speed);
    }

    private void CheckThrow(PlayerMainScipt player)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            carriedItem.GetComponent<Rigidbody>().isKinematic = false;
            carriedItem.GetComponent<Rigidbody>().useGravity = true;
            carriedItem.transform.SetParent(null);
            carriedItem.GetComponent<Rigidbody>().AddForce((player.transform.forward + player.transform.up)* player.throwStrength , ForceMode.Force);
            player.SwitchPlayerState(player.plMove);
        }
    }
}
