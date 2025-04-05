using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDownState : EnemyState
{
    Enemy_Boss enemy;

    public BossDownState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsGroundDetected())
        {
            enemy.stateMachine.ChangeState(enemy.downAttackState);
        }

        enemy.SetVelocity(0, -enemy.downSpeed);
    }
}
