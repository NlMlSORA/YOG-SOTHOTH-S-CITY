using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    
    Transform firstTransform;

    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        firstTransform = player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected() && hasChanged == false)
        {
            player.SetZeroVelocity();
        }

        if (xInput != 0 && !player.isBusy)
        {
            hasChanged = true;
            player.stateMachine.ChangeState(player.runState);
        }
    }
}
