using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHideState : EnemyState
{
    Enemy_Boss enemy;
    SpriteRenderer sp;

    public BossHideState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.canHurt = false;

        stateTimer = enemy.hideTimer;
        triggerCalled = true;


        sp = enemy.GetComponentInChildren<SpriteRenderer>();
        Color currentColor = sp.color;
        currentColor.a = 0;
        sp.color = currentColor;

        float randomX = Random.Range(-enemy.middleDistance, enemy.middleDistance);
        Vector3 newPosition = new Vector2(enemy.bossMiddlePosition.position.x + randomX, enemy.transform.position.y);
        enemy.transform.position = newPosition;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.canHurt = true;

        Color currentColor = sp.color;
        currentColor.a = 255;
        sp.color = currentColor;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0 && triggerCalled)
        {
            triggerCalled = false;
            int a = Random.Range(0, 2);

            if (a == 0)
            {
                stateMachine.ChangeState(enemy.sweepAttackState);
                return;
            }
            else
            {
                stateMachine.ChangeState(enemy.idleState);
                return;
            }

            




        }
    }
}
