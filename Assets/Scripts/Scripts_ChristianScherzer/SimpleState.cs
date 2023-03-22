using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleState : ScriptableObject
{

    public string stateName;

    /// <summary>
    /// Gets called when state is entered
    /// </summary>
    /// <param name="source"></param>
    public virtual void EnterState(GameObject source)
    {

    }

    /// <summary>
    /// Gets called when current state is this state
    /// </summary>
    public virtual void UpdateState()
    {

    }

    /// <summary>
    /// Gets called for the current state and checks Transitions to other state from this state
    /// </summary>
    /// <returns></returns>
    public virtual string Transition()
    {
        return stateName;
    }

    /// <summary>
    /// Gets called when exiting state
    /// </summary>
    public virtual void ExitState()
    {

    }

    /// <summary>
    /// Gets called everytime regadless of current state
    /// </summary>
    /// <returns></returns>
    public virtual string AnyTransition()
    {
        return "";
    }
}
