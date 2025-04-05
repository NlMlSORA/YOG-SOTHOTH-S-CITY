using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSweepAttackState : EnemyState
{
    Enemy_Boss enemy;

    float xDistance = 3;
    float yDistance = 3;
    float xSpeed = 20;
    float ySpeed = -30;

    bool attacking;


    public BossSweepAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.rb.gravityScale = 0;



        if (player.transform.position.x < enemy.bossMiddlePosition.position.x)
        {
            if (enemy.facingRight)
            {
                enemy.Flip();
            }
            enemy.transform.position = new Vector2(PlayerManager.instance.player.transform.position.x + xDistance, PlayerManager.instance.player.transform.position.y + yDistance);

        }
        else
        {
            if (!enemy.facingRight)
            {
                enemy.Flip();
            }
            enemy.transform.position = new Vector2(PlayerManager.instance.player.transform.position.x - xDistance, PlayerManager.instance.player.transform.position.y + yDistance);

        }




    }

    public override void Exit()
    {
        base.Exit();
        enemy.rb.gravityScale = 10;
        attacking = false;
    }

    public override void Update()
    {
        base.Update();


        if (enemy.IsGroundDetected())
        {
            attacking = false;
            if (triggerCalled)
            {
                enemy.stateMachine.ChangeState(enemy.idleState);
                return;
            }
            else
            {
                enemy.SetZeroVelocity();
            }
        }

        if (attacking)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Player>() != null)
                {
                    if (!PlayerManager.instance.isDead)
                    {
                        float distance = enemy.transform.position.x - hit.transform.position.x;//判断敌人和玩家的左右
                        hit.GetComponent<Player>().Damage(distance);
                        PlayerStats _target = hit.GetComponent<PlayerStats>();
                        enemy.stats.DoDamage(_target);
                    }
                    attacking = false;
                }
            }
    
        }

        
    }

    public void SetVelocity()
    {
        if (enemy.facingRight)
        {
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else
        {
            rb.velocity = new Vector2(-xSpeed, ySpeed);
        }
        attacking = true;
    }
}
