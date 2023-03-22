using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Markus Schwalb
/// Verfolge den Käse und ess ihn 
/// </summary>
public class MouseCheeseState : MouseBaseState
{
    private float timer;
    private float counter;

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        //set speed
        Mouse.navMeshMouseAgent.speed = 6;

        //set destination
        Mouse.navMeshMouseAgent.SetDestination(Mouse.cheese.transform.position);
        //Debug.Log("Cheese");
        
        //reset timer
        timer = 5;
        counter = 0;
        //play Ohh käse
        Mouse.PlayVoiceLines(Mouse.voiceLines[2]);
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        //if mouse is close to the Cheese jump and trigger Animation
        if (Vector3.Distance(Mouse.cheese.transform.position, Mouse.transform.position) < 3)
        {
            Mouse.mouseAnimator.SetBool("EatCheese", true);
            counter += 1 * Time.deltaTime;

            //after 5 seconds eating is done and go back to idle
            if (counter > timer)
            {
                Mouse.SwitchMouseState(Mouse.mouseIdle);
            }
        }
    }

    /// <summary>
    /// Destroy the cheese after eating it and set Cheese to null so you can eat other cheese
    /// and set eatCheese to false so the animation goes back to the movement blend tree
    /// </summary>
    /// <param name="Mouse"></param>
    public override void ExitMouseState(MouseStateManager Mouse)
    {
        Mouse.mouseAnimator.SetBool("EatCheese", false);
        GameObject.Destroy(Mouse.cheese);
        Mouse.cheese = null;

    }
}
