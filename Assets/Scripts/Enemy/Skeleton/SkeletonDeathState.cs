using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeathState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonDeathState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isDead = true;
        CurrencyMgr.Instance.Gold += 100;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            enemy.SetZeroVelocity();
            enemy.rb.gravityScale = 0;
            enemy.cd.enabled = false;
        }
    }
}
