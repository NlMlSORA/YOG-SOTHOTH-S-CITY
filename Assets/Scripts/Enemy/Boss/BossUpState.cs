using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUpState : EnemyState
{
    Enemy_Boss enemy;

    public BossUpState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.transform.position = new Vector2(enemy.bossMiddlePosition.position.x, enemy.transform.position.y);

        enemy.FaceToPlayer();

        enemy.rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.canUp = false;
    }

    public override void Update()
    {
        base.Update();
        if (enemy.isUpDetected())
        {
            enemy.stateMachine.ChangeState(enemy.upAttackState);
            return;
        }

        if (enemy.canUp == true)
        {
            enemy.SetVelocity(0, enemy.upSpeed);
        }
    }
}
