using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    public Enemy_Skeleton enemy;

    public SkeletonGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemy, _stateMachine, _animBoolName)
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

        if (enemy.IsPlayerDetected() && !PlayerManager.instance.isDead)
        {
            stateMachine.ChangeState(enemy.battleState);
        }


    }
}
