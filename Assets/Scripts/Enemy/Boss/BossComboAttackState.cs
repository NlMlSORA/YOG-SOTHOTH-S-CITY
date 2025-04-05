using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossComboAttackState : EnemyState
{
    Enemy_Boss enemy;

    private int comboCounter = 0;
    private float attackDir;

    public BossComboAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();



        //if (player.transform.position.x < enemy.bossMiddlePosition.position.x)
        //{
        //    enemy.transform.position = new Vector2(player.transform.position.x + 3, enemy.transform.position.y);
        //}
        //else
        //{
        //    enemy.transform.position = new Vector2(player.transform.position.x - 3, enemy.transform.position.y);
        //}

        //attackDir = enemy.facingDir;
        enemy.FaceToPlayer();

        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
        enemy.anim.SetInteger("ComboCounter", comboCounter);

        


    }

    

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();


        if(enemy.facingRight)
        {
            enemy.SetVelocity(5, 0);
        }
        else
        {
            enemy.SetVelocity(-5, 0);
        }


        if (triggerCalled)
        {
            triggerCalled = false;
            if (comboCounter < 2)
            {
                enemy.FaceToPlayer();
            }

            if (comboCounter == 2)   
            {
                enemy.stateMachine.ChangeState(enemy.idleState);
            }
            comboCounter++;
            enemy.anim.SetInteger("ComboCounter", comboCounter);
        }
    }

    
}
