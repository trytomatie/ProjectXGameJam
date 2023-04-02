using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryState : PlayerBaseState
{
    private Vector3 playerVelocity;

    GameObject carriedItem;
    private float horizontalInput;
    private float verticalInput;
    float turnSmoothVelocity = 3;
    float turnSmoothTime = 0.1f;
    float gravityValue = -9.81f;
    float countdown;

    bool groundedPlayer;
    float sprintMultiply;

    public override void EnterPlayerState(PlayerMainScipt player)
    {
        countdown = 0;
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
            carriedItem.GetComponent<Rigidbody>().isKinematic = false;
            carriedItem.transform.SetParent(null);
            //carriedItem.GetComponent<Rigidbody>().AddForce(carriedItem.transform.up, ForceMode.Force);
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
        countdown += 1 * Time.deltaTime;
        CheckThrow(player);
        if (countdown>1) CheckRelease(player);

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

        //add Gravity 

        //if the player is on the ground and his downward movement is not 0 jet change it to 0
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        RaycastHit hit;

        // if the player presses the Jump Button and the Character is on the ground than Jump

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(player.jumpHeight * -3.0f * gravityValue);
            player.plAnimator.SetTrigger("Jump");
        }

        //Check landing
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit, player.playerHeight + 0.2f, player.groundLayer) && groundedPlayer == false)
        {
            player.plAnimator.SetTrigger("Landing");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        player.plCharacterController.Move(playerVelocity * Time.deltaTime);
    }

    private void CheckRelease(PlayerMainScipt player)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.SwitchPlayerState(player.plMove);
        }
    }

    private void CheckThrow(PlayerMainScipt player)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            carriedItem.GetComponent<Rigidbody>().isKinematic = false;
            carriedItem.GetComponent<Rigidbody>().useGravity = true;
            carriedItem.transform.SetParent(null);
            carriedItem.GetComponent<Rigidbody>().AddForce((player.transform.forward + player.transform.up)* player.throwStrength , ForceMode.Force);
            player.SwitchPlayerState(player.plMove);
        }
    }
}
