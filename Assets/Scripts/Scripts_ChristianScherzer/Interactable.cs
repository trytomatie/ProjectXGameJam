using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionName;
    public Vector3 labelOffset;
    private bool isReachable;

    /// <summary>
    /// Method of Interaction
    /// </summary>
    /// <param name="soruce"></param>
    public virtual void Interaction(GameObject soruce)
    {

    }

    /// <summary>
    /// Method that triggers the Animation of Interaction
    /// </summary>
    /// <param name="source"></param>
    public virtual void TriggerAnimation(GameObject source)
    {

    }


    public bool IsReachable
    {
        get => isReachable;
        set
        {
            if (value != isReachable)
            {

            }
            isReachable = value;
        }
    }


}

