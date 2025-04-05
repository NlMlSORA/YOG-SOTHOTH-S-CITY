using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (player.IsGroundDetected())
        {
            player.SetZeroVelocity();
            player.rb.gravityScale = 0;
            player.cd.enabled = false;
            
        }

        if (triggerCalled)
        {
            PlayerManager.instance.DeadFontDisplay();
        }

        
    }
}
