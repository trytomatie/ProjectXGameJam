using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// this is the State Blueprint for the Player States
/// script by Markus
/// </summary>
public abstract class PlayerBaseState
{
    public abstract void EnterPlayerState(PlayerMainScipt player);

    public abstract void UpdatePlayerState(PlayerMainScipt player);

    public abstract void ExitPlayerState(PlayerMainScipt player);

    public abstract void PlayerTriggerEnter(PlayerMainScipt player);

    public abstract void PlayerTriggerExit(PlayerMainScipt player);

    public abstract void PlayerTriggerStay(PlayerMainScipt player);

}
