using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    public Enemy_Skeleton enemy;
    private Transform playerTransform;
    private int moveDir;
    


    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (PlayerManager.instance.isDead)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if(playerTransform.position.y - enemy.transform.position.y > 3 && player.IsGroundDetected())
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                enemy.SetZeroVelocity();
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                    return;
                }

                stateMachine.ChangeState(enemy.idleState);
                return;
            }
        }
        //else
        //{
        //    stateMachine.ChangeState(enemy.idleState);
        //}

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (playerTransform.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (playerTransform.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        enemy.SetVelocity(moveDir * enemy.moveSpeed, rb.velocity.y);


    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastAttackTime + enemy.attackCooldownTime)
        {
            enemy.lastAttackTime = Time.time;
            return true;
        }

        return false;
    }
}
