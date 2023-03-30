using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{

    private float horizontalInput;
    private float verticalInput;

    public override void EnterPlayerState(PlayerMainScipt player)
    {
    }

    public override void ExitPlayerState(PlayerMainScipt player)
    {
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
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Get the camera's forward vector and flatten it
        Vector3 cameraForward = Vector3.Scale(player.camer.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (horizontalInput * player.camer.right + verticalInput * cameraForward).normalized;

        player.plCharacterController.Move(direction * player.speed * Time.deltaTime);
        //noise = 3;

        //Animation
        player.plAnimator.SetFloat("MoveHorizontal", direction.z * player.speed);
        player.plAnimator.SetFloat("MoveVertical", direction.x * player.speed);
    }
}
