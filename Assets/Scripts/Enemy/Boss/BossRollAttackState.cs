using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollAttackState : EnemyState
{
    Enemy_Boss enemy;
    public bool isTop = false;
    bool canDown = false;
    bool attacking = false;

    public BossRollAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        enemy.FaceToPlayer();

        enemy.rb.gravityScale = 0;

        if (enemy.facingRight)
        {
            rb.velocity = new Vector2(10, 30);
        }
        else
        {
            rb.velocity = new Vector2(-10, 30);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.rb.gravityScale = 10;
        enemy.anim.SetBool("RollAttackDown", false);
    }

    public override void Update()
    {
        base.Update();
        if (isTop)
        {
            isTop = false;
            enemy.SetZeroVelocity();
            stateTimer = 0.01f;
            canDown = true;
            attacking = true;
        }

        if (triggerCalled)
        {
            triggerCalled = false;
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

        if (canDown)
        {
            if(stateTimer < 0)
            {
                canDown = false;
                enemy.anim.SetBool("RollAttackDown", true);
                if (enemy.facingRight)
                {
                    rb.velocity = new Vector2(20, -80);
                }
                else
                {
                    rb.velocity = new Vector2(-20, -80);
                }



                if (enemy.IsGroundDetected() && attacking == true)
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
                            
                        }
                    }

                    attacking = false;
                }
            }
        }
    }
}
