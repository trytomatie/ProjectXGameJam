using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDraggingState : PlayerBaseState
{
    

    private float horizontalInput;
    private float verticalInput;
    float turnSmoothVelocity = 3;
    float turnSmoothTime = 0.1f;
    //float gravityValue = -9.81f;

    bool groundedPlayer;
    float sprintMultiply;
    public override void EnterPlayerState(PlayerMainScipt player)
    {
        

    }

    public override void ExitPlayerState(PlayerMainScipt player)
    {
        if (player.dragHelper.transform.childCount > 0)
        {
            player.dragItem = null;
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
        Vector3 direction2 = player.dragHelper.transform.position - player.dragItem.transform.position;

        player.dragItem.GetComponent<Rigidbody>().AddForce(direction2*200, ForceMode.Force);

        CheckStayState(player);


        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Get the camera's forward vector and flatten it
        Vector3 cameraForward = Vector3.Scale(player.camer.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (horizontalInput * player.camer.right + verticalInput * cameraForward).normalized;

        player.plCharacterController.Move(direction * (player.speed / 2f) * Time.deltaTime);
        //noise = 3;



        //rotate with camera angle
        float targetAngle = player.camer.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        player.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        //Animation
        player.plAnimator.SetFloat("MoveHorizontal", horizontalInput * player.speed);
        player.plAnimator.SetFloat("MoveVertical", verticalInput * player.speed);
    }

    private void CheckStayState(PlayerMainScipt player)
    {
        if (Input.GetMouseButtonUp(1))
        {
            player.SwitchPlayerState(player.plMove);
        }
        if (Vector3.Distance(player.dragHelper.transform.position, player.dragItem.transform.position) > 3)
        {
            player.SwitchPlayerState(player.plMove);
        }
    }
}
