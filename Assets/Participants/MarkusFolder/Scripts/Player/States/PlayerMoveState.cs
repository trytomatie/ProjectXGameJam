using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class PlayerMoveState : PlayerBaseState
{
    private Vector3 playerVelocity;

    private float horizontalInput;
    private float verticalInput;
    float turnSmoothVelocity=3;
    float turnSmoothTime = 0.1f;
    float gravityValue = -9.81f;

    bool groundedPlayer;
    float sprintMultiply;
    public override void EnterPlayerState(PlayerMainScipt player)
    {
        
    }
    public override void UpdatePlayerState(PlayerMainScipt player)
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        CheckSprint();

        //Get the camera's forward vector and flatten it
        Vector3 cameraForward = Vector3.Scale(player.camer.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (horizontalInput * player.camer.right + verticalInput * cameraForward).normalized;

        player.plCharacterController.Move(direction * (player.speed * sprintMultiply) * Time.deltaTime);
        //noise = 3;



        //rotate with camera angle
        float targetAngle = player.camer.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        player.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        //Animation
        player.plAnimator.SetFloat("MoveHorizontal", horizontalInput * player.speed);
        player.plAnimator.SetFloat("MoveVertical", verticalInput * player.speed);

        checkJump(player);
        checkDrag(player);
    }

    private void checkDrag(PlayerMainScipt player)
    {
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 3, player.plLayerMask))
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.CompareTag("DraggAble") && Input.GetMouseButton(1))
            {
                Debug.Log("initialize plDragg");

                player.dragItem = hit.collider.gameObject;
                player.SwitchPlayerState(player.plDragg);
            }
        }
    }

    private void CheckSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintMultiply = 2;
        } else
        {
            sprintMultiply = 1;
        }
    }

    private void checkJump(PlayerMainScipt player)
    {

        // cast a raycast downwards from the object's position
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit, player.playerHeight, player.groundLayer))
        {
            if (!groundedPlayer)
            {
                player.plAnimator.SetTrigger("Landed");
            }
            
            groundedPlayer = true;
            
        }
        else
        {
            groundedPlayer = false;
        }



        //add Gravity 

        //if the player is on the ground and his downward movement is not 0 jet change it to 0
        if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

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
        if (other.CompareTag("CarryAble"))
        {
            if (AngleBetween(player, other)<90 && Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.SetParent(player.carryHelperObject);
                other.GetComponent<Rigidbody>().useGravity = false;
                player.SwitchPlayerState(player.plCarry);
            }
        }
        /*if (other.CompareTag("DraggAble"))
        {
            if (AngleBetween(player, other) < 90 && Input.GetMouseButtonDown(1))
            {
                //other.gameObject.transform.SetParent(player.dragHelper.transform
                player.dragItem = other.gameObject;
                player.SwitchPlayerState(player.plDragg);
            }
        }*/
    }

    private float AngleBetween(PlayerMainScipt player, Collider other)
    {
        Vector3 vectorBetween = other.transform.position - player.transform.position;
        Vector2 vector2Between = new Vector2 (vectorBetween.x, vectorBetween.y);
        Vector2 playerVector2 = new Vector2 (player.transform.forward.x, player.transform.forward.y);
        //Debug.Log(Vector2.Angle(playerVector2, vectorBetween));
        return Vector2.Angle(playerVector2, vector2Between) ;
    }
}
