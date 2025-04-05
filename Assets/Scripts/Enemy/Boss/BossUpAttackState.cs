using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUpAttackState : EnemyState
{
    Enemy_Boss enemy;
    bool canDown = false;

    public BossUpAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = enemy.gravity;
        canDown = false;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            triggerCalled = false;
            stateTimer = .2f;
            canDown = true;
        }

        if (canDown)
        {
            if (stateTimer < 0)
            {
                enemy.stateMachine.ChangeState(enemy.downState);
            }
        }


    }
}
