using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.UI;

public class PlayerTargetState : PlayerBaseState
{

    private float horizontalInput;
    private float verticalInput;

    private float mouseX;
    private float mouseY;

    private Vector2 targetLockOffset = new Vector2 (1,1);


    Vector3 lastTargetPosition ; // Store the last position of the target for velocity calculation
    private int numRaycasts = 5; // Number of raycasts to use
    private float maxRaycastDistance = 100f; // Maximum distance for raycasts
    private float coneAngle = 30f; // Angle of the cone in degrees
    private float coneOffset = 0.1f; // Distance between raycasts in the cone

    public override void EnterPlayerState(PlayerMainScipt player)
    {
        Debug.Log("Target State");
        player.virtualCmCam.m_XAxis.m_InputAxisName = "";
        player.virtualCmCam.m_YAxis.m_InputAxisName = "";

        Vector3 lastTargetPosition = player.targetObject.transform.position; // Store the last position of the target for velocity calculation

    }

    public override void ExitPlayerState(PlayerMainScipt player)
    {
        player.virtualCmCam.m_XAxis.m_InputAxisName = "Mouse X";
        player.virtualCmCam.m_YAxis.m_InputAxisName = "Mouse Y";
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
        CheckTarget(player);
        CheckExit(player);
        if (player.targetObject)
        {
            MoveCamera(player);

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

    private void CheckTarget(PlayerMainScipt player)
    {
        if (!player.targetObject)
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
                    float distance = Vector3.Distance(player.transform.position, hit.collider.transform.position); // Calculate the distance to the hit object
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        player.targetObject = hit.collider.gameObject;
                        player.SwitchPlayerState(player.plTarget);
                    }
                }
            }
            if (!player.targetObject)
            {
                player.SwitchPlayerState(player.plMove);
            }
        }
    }

    private void CheckExit(PlayerMainScipt player)
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            player.SwitchPlayerState(player.plMove);
        }
    }

    private void MoveCamera(PlayerMainScipt player)
    {
        /*if (!player.targetObject) return;

        //Vector3 viewPos = player.mainCam.WorldToViewportPoint(player.targetObject.transform.position);

        

        //if ((player.targetObject.transform.position - player.transform.position).magnitude < 10) return;
        mouseX = (player.targetObject.transform.position.x - 0.5f + targetLockOffset.x) * 3f;              // you can change the [ 3f ] value to make it faster or  slower
        mouseY = (player.targetObject.transform.position.y - 0.5f + targetLockOffset.y) * 3f;              // don't use delta time here.

        
        player.virtualCmCam.m_YAxis.m_InputAxisValue = mouseY;
        player.virtualCmCam.m_XAxis.m_InputAxisValue = mouseX;*/
        /*float sensitivity = 3f;

        // Calculate the direction from the player to the target
        Vector3 direction = player.targetObject.transform.position - player.transform.position;

        // Calculate the angles to rotate the camera based on the direction
        float mouseX = direction.normalized.x * sensitivity;
        float mouseY = -Mathf.Atan2(direction.normalized.y, direction.normalized.z) * Mathf.Rad2Deg * sensitivity;*/

        // Set the input values of the camera's X and Y axes
       

        player.virtualCmCam.m_XAxis.m_InputAxisValue = horizontalInput;
        player.virtualCmCam.m_YAxis.m_InputAxisValue = verticalInput;
    }
}

