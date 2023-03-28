using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseIsBlended : MouseBaseState
{
    public override void EnterMouseState(MouseStateManager Mouse)
    {
        //Change eye Material
        Mouse.eyeRenderer.material = Mouse.eyeMaterials[1];
        //start to run
        Mouse.navMeshMouseAgent.speed = 6;
        //set destination to the location where you saw the player
        
        Mouse.navMeshMouseAgent.SetDestination(CreateRunToPoint(Mouse));
        Mouse.blendAble = false;
        Debug.Log("Is Blended State");
    }


    public override void ExitMouseState(MouseStateManager Mouse)
    {
        Mouse.blendTime = 5;
        Mouse.blendAble = true;
        //Change eye Material
        Mouse.eyeRenderer.material = Mouse.eyeMaterials[0];
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        //check if player is to close
        CheckForNoise(Mouse);
        
        //if the Enemy is on the 


        // Switch state
        if (timer(Mouse))
        {
            Mouse.SwitchMouseState(Mouse.mouseySearch);
        }
    }

    private bool timer(MouseStateManager Mouse)
    {
        Mouse.blendTime -= (1 * Time.deltaTime);
        if  (Mouse.blendTime<0)
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    private void CheckForNoise(MouseStateManager Mouse)
    {

        // player = Mouse.player.GetComponent<PlayerController>();
        //if (player.noise > distance /*&& Mouse.currentState != Mouse.mouseCheese*/)
        //{
        //    Mouse.SwitchMouseState(Mouse.mChase);
        //}
    }

    private Vector3 CreateRunToPoint(MouseStateManager Mouse)
    {
        //inteded that blinded Enemys run simply to the the Point that is infront of them it could cause problems if there are things in the way that doesnt block hte ray cast
        Vector3 direction = Mouse.player.transform.position - Mouse.transform.position;
        Ray ray = new Ray(Mouse.transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, 20, Mouse.mouseRayCastLayers))
        {
            // A collider was hit
            
            Debug.Log("Hit object: " + hit.transform.name);
            // Do something with the hit object
            return (hit.transform.position);
        }
        else
        {
            // No collider was hit
            Debug.Log("No object hit");
            return (Mouse.transform.position + (direction * 6));
        }

        
    }
}
