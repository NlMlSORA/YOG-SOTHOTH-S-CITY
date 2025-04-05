using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunedState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonStunedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.fX.InvokeRepeating("RedColorBlink", 0, 0.1f);

        stateTimer = enemy.stunedDuration;
        rb.velocity = new Vector2(enemy.stunedDirection.x * -enemy.facingDir, enemy.stunedDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.fX.Invoke("CancelRedColorBlink", 0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }


}
