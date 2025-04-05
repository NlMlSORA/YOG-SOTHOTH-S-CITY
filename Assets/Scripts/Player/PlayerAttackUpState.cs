using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackUpState : PlayerState
{
    public PlayerAttackUpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //rb.gravityScale = 0;
        //player.SetZeroVelocity();

        AfterImageEffect.instance.StartAfterImageEffect();
    }

    public override void Exit()
    {
        base.Exit();
        //rb.gravityScale = player.gravity;

        AfterImageEffect.instance.EndAfterImageEffect();
    }

    public override void Update()
    {
        base.Update();
        //player.SetZeroVelocity();
        if (triggerCalled)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
