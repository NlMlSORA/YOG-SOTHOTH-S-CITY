using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName, _skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(0, enemy.rb.velocity.y);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
