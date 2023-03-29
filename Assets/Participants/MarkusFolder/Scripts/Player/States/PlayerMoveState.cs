using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMoveState : PlayerBaseState
{
    private float horizontalInput;
    private float verticalInput;
    float turnSmoothVelocity=3;
    float turnSmoothTime = 0.1f;

    public override void EnterPlayerState(PlayerMainScipt player)
    {
        
    }
    public override void UpdatePlayerState(PlayerMainScipt player)
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Get the camera's forward vector and flatten it
        Vector3 cameraForward = Vector3.Scale(player.camer.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (horizontalInput * player.camer.right + verticalInput * cameraForward).normalized;

        player.plCharacterController.Move(direction * player.speed * Time.deltaTime);
        //noise = 3;



        //rotate with camera angle
        float targetAngle = player.camer.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        player.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        //Animation
        player.plAnimator.SetFloat("MoveHorizontal", horizontalInput/* * player.speed*/);
        player.plAnimator.SetFloat("MoveVertical", verticalInput /** player.speed*/);
    }

    public override void ExitPlayerState(PlayerMainScipt player)
    {
        
    }

    public override void PlayerTriggerEnter(PlayerMainScipt player)
    {
        
    }

    public override void PlayerTriggerExit(PlayerMainScipt player)
    {
        
    }

    public override void PlayerTriggerStay(PlayerMainScipt player)
    {
        
    }

    
}
