using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseySearchState : MouseBaseState
{
    /// <summary>
    /// Markus Schwalb
    /// in this state Mousey should walk to randem Position near the start Position
    /// </summary>
    /// 
    private Vector3[] lookpoints = new Vector3[6];
    private Vector3 startPoint;
    private int nextPoint;
    private float timer;
    private float counter;

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        //reset Timer
        timer = 10;
        counter = 1;
        //Debug.Log("Searchstate");
        //Debug.Log("Lookpoints.length " + lookpoints.Length);

        //Set Speed
        Mouse.navMeshMouseAgent.speed = 2.5f;

        //Set Animation
        //Mouse.mouseAnimator.SetBool("isSneaking", true);
        Mouse.viewHeight = -1;

        //initialize lookpoints
        startPoint = Mouse.transform.position;
        nextPoint = 0;
        for (int i = 0; i < lookpoints.Length; i++)
        {
            float rando = Random.Range(-5,     5);
            lookpoints[i] = new Vector3 (startPoint.x + rando, startPoint.y, startPoint.z + rando);
            //Debug.Log(lookpoints[i]);
        }
        //Debug.Log(lookpoints);
        Mouse.navMeshMouseAgent.SetDestination(lookpoints[nextPoint]);

        //play wo bist du
        Mouse.PlayVoiceLines(Mouse.voiceLines[3]);
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        counter += 1 * Time.deltaTime;
        //wenn du an einem Punkt angekommen bist setze den nächsten lookpoint als dein ziel 
        if ( Mouse.navMeshMouseAgent.remainingDistance <= 0.3f && nextPoint < lookpoints.Length-1)
        {
            
            nextPoint++;
            //Debug.Log("next LookPoint: " + lookpoints[nextPoint]);
            Mouse.navMeshMouseAgent.SetDestination(lookpoints[nextPoint]);
            //Debug.Log(nextPoint);

             // wenn du den Array durchgegeangen bist oder Timer zuende ist geh in Den Idle/Wait state
        } else if (nextPoint >= lookpoints.Length-2 || timer < counter)
        {
            Debug.Log("fertig gesearched");
            Mouse.SwitchMouseState(Mouse.mouseIdle);
        }

    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
        //exit sneaking Movement
        //.mouseAnimator.SetBool("isSneaking", false);
        Mouse.viewHeight = 1;
    }

}
