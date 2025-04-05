using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{

    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //攀墙跳
        //if (Input.GetKeyDown(KeyCode.K) && player.IsWallDetected())
        //{
        //    player.stateMachine.ChangeState(player.wallJumpState);
        //    return;
        //}

        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.J))
        {
            // 触发上劈动画
            player.stateMachine.ChangeState(player.attackUpState);
            return;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.J))
        {
            player.stateMachine.ChangeState(player.attackDownState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            player.stateMachine.ChangeState(player.airAttackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.U) && SkillManager.instance.farAttack.CanUseSkill())
        {
            stateMachine.ChangeState(player.farAttackState);
            return;
        }

    }
}
