using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : EnemyState
{
    Enemy_Boss enemy;
    bool flag = false;

    public BossIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.FaceToPlayer();
        stateTimer = enemy.idleTime;
        flag = false;
        
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(0, enemy.rb.velocity.y);


        if (stateTimer < 0 && flag == false && !PlayerManager.instance.isDead)
        {
            //enemy.stateMachine.ChangeState(enemy.comboAttackState);

            //enemy.stateMachine.ChangeState(enemy.upState);

            //enemy.stateMachine.ChangeState(enemy.rollAttackState);

            //enemy.stateMachine.ChangeState(enemy.sweepAttackState);
            flag = true;
            enemy.RandomAttack();
            
        }

    }
}
