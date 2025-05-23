using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        //if (player.IsWallDetected())
        //{
        //    stateMachine.ChangeState(player.wallSlideState);
        //}

        if (player.IsGroundDetected())
        {
            player.GetComponentInChildren<PlayerAnimations>().RunAudio();
            stateMachine.ChangeState(player.idleState);
        }
    }
}
