using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAirState
{
    bool flag = false;

    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .2f;
        
    }

    public override void Exit()
    {
        base.Exit();
        flag = false;
    }

    public override void Update()
    {
        base.Update();

        if (!flag)
        {
            player.SetVelocity(5 * -player.facingDir, player.jumpForce);
            flag = true;
        }
        
        if (rb.velocity.y < 0)
        {
            player.stateMachine.ChangeState(player.fallState);
        }

        if (stateTimer < 0 && xInput != 0)
        {
            player.stateMachine.ChangeState(player.fallState);
        }
    }
}
