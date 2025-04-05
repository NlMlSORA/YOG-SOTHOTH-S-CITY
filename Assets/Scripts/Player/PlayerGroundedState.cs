using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    

    protected bool hasChanged = false;

    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hasChanged = false;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.J))
        {
            // 触发上劈动画
            player.stateMachine.ChangeState(player.attackUpState);
            return;
        }

        if (!player.IsGroundDetected())
        {
            player.stateMachine.ChangeState(player.fallState);
            return;
        }

        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    stateMachine.ChangeState(player.aimState);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    stateMachine.ChangeState(player.counterAttack);
        //}

        if (Input.GetKeyDown(KeyCode.J))
        {
            stateMachine.ChangeState(player.primaryAttackState);
            return;
        }

        //if (!player.IsGroundDetected())
        //{
        //    stateMachine.ChangeState(player.airState);
        //}

        if (Input.GetKeyDown(KeyCode.K) && player.IsGroundDetected())
        {
            hasChanged = true;
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.U) && SkillManager.instance.farAttack.CanUseSkill())
        {
            stateMachine.ChangeState(player.farAttackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.O) && player.stats.currentHealth < player.stats.maxHealth.GetValue() && SkillManager.instance.energy >= SkillManager.instance.energyNeed)
        {
            stateMachine.ChangeState(player.healState);
        }
    }
}
