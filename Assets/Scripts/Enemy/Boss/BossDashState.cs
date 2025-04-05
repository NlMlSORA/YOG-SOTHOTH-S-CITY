using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BossDashState : EnemyState
{
    Enemy_Boss enemy;
    SpriteRenderer sp;

    public BossDashState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.FaceToPlayer();
        //stateTimer = enemy.dashDuration;

        //enemy.rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetVelocity(0, 0);
        
    }

    public override void Update()
    {
        base.Update();

        

        //if (stateTimer < 0)
        //{
        //    enemy.SetZeroVelocity();
        //    if (stateTimer < -0.15)
        //    {
        //        //if (enemy.IsGroundDetected())
        //        //{
        //        stateMachine.ChangeState(enemy.idleState);
        //        //}
        //        //else
        //        //{
        //        //    stateMachine.ChangeState(enemy.fallState);
        //        //}
        //    }
        //}

        if (triggerCalled)
        {
            
            stateMachine.ChangeState(enemy.hideState);
            

        }

        else
        {
            enemy.SetVelocity(enemy.dashSpeed * enemy.facingDir, 0);
        }
    }
}
