using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDownAttackState : EnemyState
{
    Enemy_Boss enemy;

    public BossDownAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.FaceToPlayer();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.rb.gravityScale = 10f;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

    }
}
