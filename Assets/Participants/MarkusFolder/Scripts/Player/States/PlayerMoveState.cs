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

    private int numRaycasts = 5; // Number of raycasts to use
    private float maxRaycastDistance = 100f; // Maximum distance for raycasts
    private float coneAngle = 30f; // Angle of the cone in degrees
    private float coneOffset = 0.1f; // Distance between raycasts in the cone

    private Vector3[] raycastPositions; // Array to store the starting positions of raycasts
    public override void EnterPlayerState(PlayerMainScipt player)
    {
        

        //create Raycast positions
        raycastPositions = new Vector3[numRaycasts];
        /*for (int i = 0; i < numRaycasts; i++)
        {
            float angle = (i / (float)(numRaycasts - 1)) * coneAngle - (coneAngle / 2f); // Calculate the angle for this raycast
            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * player.camer.transform.forward; // Calculate the direction for this raycast
            Vector3 position = player.camer.transform.position + direction * coneOffset; // Calculate the starting position for this raycast
            raycastPositions[i] = position;
        }*/
    }
    public override void UpdatePlayerState(PlayerMainScipt player)
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
      //  Debug.Log("Mouse X axis: " + mouseX);
       // Debug.Log("Mouse Y axis: " + mouseY);
        
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

        CheckJump(player);
        CheckInFront(player);
        CheckTarget(player);
    }

    private void CheckTarget(PlayerMainScipt player)
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            float closestDistance = Mathf.Infinity; // Reset the closest distance
            player.targetObject = null;

            for (int i = 0; i < numRaycasts; i++)
            {
                float angle = (i / (float)(numRaycasts - 1)) * coneAngle - (coneAngle / 2f); // Calculate the angle for this raycast
                Vector3 direction = Quaternion.Euler(0f, angle, 0f) * player.camer.transform.forward; // Calculate the direction for this raycast
                Vector3 position = player.camer.transform.position + direction * coneOffset; // Calculate the starting position for this raycast
                RaycastHit hit;
                if (Physics.Raycast(position, direction, out hit, maxRaycastDistance, player.targetLayer))
                {
                    Debug.Log("HitTargetLayer");
                    float distance = Vector3.Distance(player.transform.position, hit.collider.transform.position); // Calculate the distance to the hit object
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        player.targetObject = hit.collider.gameObject;
                        
                    }
                }
            }
            if (player.targetObject)
            {
                player.SwitchPlayerState(player.plTarget);
            }

            if (player.targetObject != null)
            {
                // Do something with closest enemy, e.g. highlight or lock on
            }
        }
        
    }

    private void CheckInFront(PlayerMainScipt player)
    {
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 3, player.plLayerMask))
        {
            Debug.Log(hit.collider.gameObject+" infront");
            if (hit.collider.gameObject.CompareTag("DraggAble") && Input.GetMouseButton(1))
            {
                Debug.Log("initialize plDragg");

                player.dragItem = hit.collider.gameObject;
                player.SwitchPlayerState(player.plDragg);
            }
            if (hit.collider.gameObject.CompareTag("ClimbAble") && Input.GetMouseButton(1))
            {
                player.climbWall = hit.collider.gameObject;
                player.SwitchPlayerState(player.plClimb);
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

    private void CheckJump(PlayerMainScipt player)
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
