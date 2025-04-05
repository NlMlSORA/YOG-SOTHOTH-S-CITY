using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    private int comboCounter;
    private float lastAttackTime;
    private float comboWindow = 1;
    private float attackDir;

    public PlayerAirAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        attackDir = player.facingDir;
        if (comboCounter > 1 || Time.time > lastAttackTime + comboWindow)
        {
            comboCounter = 0;
        }
        //player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        player.anim.SetInteger("AirComboCounter", comboCounter);
        //rb.gravityScale = 0;

        AfterImageEffect.instance.StartAfterImageEffect();
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastAttackTime = Time.time;
        player.StartCoroutine("BusyFor", .15f);
        //rb.gravityScale = player.gravity;

        AfterImageEffect.instance.EndAfterImageEffect();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
        {
            attackDir = xInput;
        }



        //player.SetZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
