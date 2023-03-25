using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Markus Schwalb
/// This is the script dor Updating the Animation Mainly the Walk animation
/// </summary>

public class MouseAnimationState : MouseBaseState
{
    /// <summary>
    /// set the Current look at location
    /// </summary>
    /// <param name="Mouse"></param>
    public override void EnterMouseState(MouseStateManager Mouse)
    {
        //Mouse.lookAt.transform.position = Mouse.transform.forward * 5 + new Vector3(0,Mouse.viewHeight,0);
    }

    /// <summary>
    /// update the walk animation with the current speed Value
    /// and update look at location
    /// </summary>
    /// <param name="Mouse"></param>
    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        float speed = Mouse.navMeshMouseAgent.velocity.magnitude;
        Mouse.mouseAnimator.SetFloat("Direction", speed);
        //Debug.Log("Gonna go fast "+ Mouse.navMeshMouseAgent.velocity);

        //updateLookAt(Mouse);
        
    }

    /// <summary>
    /// update the Lookdirection of Mousey if mousey is chasing look at player otherwise look to the front
    /// </summary>
    /// <param name="Mouse"></param>
    /*private void updateLookAt(MouseStateManager Mouse)
    {
        if (Mouse.currentState == Mouse.mChase)
        {
            Mouse.lookAt.transform.position = Vector3.Lerp(Mouse.lookAt.transform.position, Mouse.player.transform.position+ new Vector3(0,0f,0), Time.deltaTime*5);
        } 
        else
        {
            Mouse.lookAt.transform.position = Vector3.Lerp(Mouse.lookAt.transform.position, Mouse.transform.position + Mouse.transform.forward * 5 + new Vector3(0, Mouse.viewHeight, 0), Time.deltaTime*5);
        }
    */

    public override void ExitMouseState(MouseStateManager Mouse)
    {
    }
}
