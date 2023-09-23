using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineTest01 : MonoBehaviour
{
    private StateTest01 currentState;

    public void SwitchState(StateTest01 newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
