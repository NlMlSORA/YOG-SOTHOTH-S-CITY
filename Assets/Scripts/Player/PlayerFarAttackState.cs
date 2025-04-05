using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarAttackState : PlayerState
{
    public bool attacking = false;

    public PlayerFarAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = 0;
        attacking = false;
        player.SetZeroVelocity();

        AfterImageEffect.instance.StartAfterImageEffect();
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = player.gravity;
        player.SetZeroVelocity();
        
    }

    public override void Update()
    {
        base.Update();
        //player.SetZeroVelocity();


        if (attacking)
        {
            if (player.facingRight)
            {
                rb.velocity = new Vector2(-3, 0);
            }
            else
            {
                rb.velocity = new Vector2(3, 0);
            }
        }
        else
        {
            player.SetZeroVelocity();
        }


        if (triggerCalled)
        {
            AfterImageEffect.instance.EndAfterImageEffect();
            player.stateMachine.ChangeState(player.idleState);
        }
    }

    public void StartFarAttack()
    {
        attacking = true;
    }
}
