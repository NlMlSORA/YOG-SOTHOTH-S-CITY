using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState;

    public void Init(EnemyState _enemyState)
    {
        currentState = _enemyState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _enemyState)
    {
        currentState.Exit();
        currentState = _enemyState;
        currentState.Enter();
    }


}
