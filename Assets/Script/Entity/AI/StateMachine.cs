using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    protected State currentState;
    
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        if (currentState == newState)
            return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
