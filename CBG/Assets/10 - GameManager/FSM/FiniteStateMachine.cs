using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private Dictionary<string, FiniteState> machine = null;
    private FiniteState currentState = null;
    private string nextStateName = null;
    private FiniteState nextState = null;

    private FiniteMachineStates fms = FiniteMachineStates.ON_ENTER;

    public FiniteStateMachine()
    {
        this.machine = new Dictionary<string, FiniteState>();
    }

    public void Add(FiniteState state)
    {
        machine.Add(state.Title, state);

        if (currentState == null)
        {
            currentState = state;
        }
    }

    public void xxxOnUpdate(float dt)
    {
        nextStateName = currentState.OnUpdate(dt);

        if (nextStateName != null)
        {
            if (machine.TryGetValue(nextStateName, out FiniteState state))
            {
                currentState.OnExit();
                currentState = state;
                currentState.OnEnter();
            }
        }
    }

    public void OnUpdate(float dt)
    {
        switch(fms)
        {
            case FiniteMachineStates.ON_ENTER:
                currentState.OnEnter();
                fms = FiniteMachineStates.ON_UPDATE;
                break;
            case FiniteMachineStates.ON_UPDATE:
                nextStateName = currentState.OnUpdate(dt);
                if (nextStateName != null)
                {
                    if (machine.TryGetValue(nextStateName, out FiniteState state))
                    {
                        fms = FiniteMachineStates.ON_EXIT;
                        nextState = state;
                    }
                }
                break;
            case FiniteMachineStates.ON_EXIT:
                currentState.OnExit();
                currentState = nextState;
                fms = FiniteMachineStates.ON_ENTER;
                break;
        }
    }
}

public enum FiniteMachineStates
{
    ON_EXIT,
    ON_ENTER,
    ON_UPDATE
}
