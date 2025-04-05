using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }


    public void InitState(PlayerState _state)
    {
        currentState = _state;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _state)
    {
        currentState.Exit();
        currentState = _state;
        currentState.Enter();
    }
}
