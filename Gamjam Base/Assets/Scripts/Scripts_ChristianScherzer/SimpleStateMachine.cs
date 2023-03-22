using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimpleStateMachine : State
{
    public List<SimpleState> states;
    public SimpleState currentState;

    // Start is called before the first frame update
    public void Start()
    {
        currentState = states[0];
        currentState.EnterState(gameObject);
    }

    public void Update()
    {
        Tick(CheckStates(currentState));
    }

    SimpleState CheckStates(SimpleState state)
    {
        string stateName = state.stateName;
        foreach (SimpleState s in states)
        {
            string name= s.AnyTransition();
            if(name != "")
            {
                stateName = name;
            }
           //  print(stateName);
        }
        if (state.stateName == stateName)
        {
            stateName = state.Transition();
        }
        if(state.stateName != stateName)
        {
            return states.First(state => state.stateName == stateName);
        }
        return state;
    }


    void Tick(SimpleState state)
    {
        currentState.UpdateState();

        if (state == currentState)
        return;

        currentState?.ExitState();
        currentState = state;
        currentState.EnterState(gameObject);
    }
}
