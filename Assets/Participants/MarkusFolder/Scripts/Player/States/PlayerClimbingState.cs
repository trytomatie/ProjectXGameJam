using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingState : PlayerBaseState
{
    private float horizontalInput;
    private float verticalInput;


    public override void EnterPlayerState(PlayerMainScipt player)
    {
        player.plAnimator.SetBool("Climbing", true);
    }

    public override void ExitPlayerState(PlayerMainScipt player)
    {
        player.plAnimator.SetBool("Climbing", false);

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
        //get Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //CheckSprint(); //maybe later

        player.plCharacterController.Move(((player.transform.right * horizontalInput)+(player.transform.up * verticalInput)) * player.speed * Time.deltaTime);

        //Animation
        player.plAnimator.SetFloat("MoveHorizontal", horizontalInput * player.speed);
        player.plAnimator.SetFloat("MoveVertical", verticalInput * player.speed);

        CheckInfront(player);
        CheckBelow(player);
        CheckButtonPressed(player);
    }

    private void CheckButtonPressed(PlayerMainScipt player)
    {
        //wenn der Klettern Button nicht mehr da ist Fall
        if (!Input.GetMouseButton(1))
        {
            player.SwitchPlayerState(player.plMove);
            //player.plAnimator.SetTrigger("Fall"); //Maybe later
        }
    }

    private void CheckBelow(PlayerMainScipt player)
    {
        //Wenn man Rinterklettert und man den Boden schon erreicht geh raus aus dem Kletter Mode
        RaycastHit hit;

        if (!Physics.Raycast(player.transform.position, Vector3.down, out hit, player.playerHeight, player.groundLayer) && verticalInput<-0.1)
        {
            player.SwitchPlayerState(player.plMove);
        }
    }

    private void CheckInfront(PlayerMainScipt player)
    {
        // Wenn man die Wand verlässt verlass den KletterMode
        RaycastHit hit;

        if (!Physics.Raycast(player.transform.position - new Vector3 (0, player.playerHeight, 0), player.transform.forward, out hit, 3, player.plLayerMask))
        {
            player.SwitchPlayerState(player.plMove);
        }
    }

    /*private void CheckSprint()
    {
        throw new NotImplementedException();
    }*/
}
