using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Markus Schwalb
/// Chase After the player
/// </summary>
public class MouseyChase : MouseBaseState
{
    /// <summary>
    /// Start to run and run to the point of the player
    /// </summary>
    /// <param name="Mouse"></param>
    public override void EnterMouseState(MouseStateManager Mouse)
    {
        //start to run
        Mouse.navMeshMouseAgent.speed = 6;
        //set destination to the location where you saw the player
        Mouse.navMeshMouseAgent.SetDestination(Mouse.player.transform.position);
        //komm doch her mein bester
        Mouse.PlayVoiceLines(Mouse.voiceLines[1]);
        //Add 1 to the player chase Index for visual feedback
        Mouse.player.GetComponent<PlayerEasyAllInOne>().chaseIndex++;



    }

    /// <summary>
    /// Change to search state if didnt catch the player and are at the location where you saw him
    /// </summary>
    /// <param name="Mouse"></param>
    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        if (Mouse.navMeshMouseAgent.remainingDistance <= 0.2f || Mouse.player.GetComponent<PlayerEasyAllInOne>().isHidden  /*&& Mouse.navMeshMouseAgent.pathStatus==NavMeshPathStatus.PathComplete*/)
        {
            Mouse.SwitchMouseState(Mouse.mouseySearch);
        }
    }

    /// <summary>
    /// subtract 1 to the chase Index of the player if chase index is greater then 0 then the volume changesusw.
    /// </summary>
    /// <param name="Mouse"></param>
    public override void ExitMouseState(MouseStateManager Mouse)
    {
        Mouse.player.GetComponent<PlayerEasyAllInOne>().chaseIndex--;
    }
}
