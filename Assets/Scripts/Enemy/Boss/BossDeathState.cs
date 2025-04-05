using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : EnemyState
{
    Enemy_Boss enemy;

    public BossDeathState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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

        if (enemy.IsGroundDetected())
        {
            enemy.SetZeroVelocity();
        }
    }
}
