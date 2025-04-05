using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Animator anim;
    protected Rigidbody2D rb;

    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected string animBoolName;

    protected bool triggerCalled;

    protected float stateTimer;

    protected Player player;


    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }


    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.anim.SetBool(animBoolName, true);
        rb = enemyBase.rb;
        player = PlayerManager.instance.player;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }


}
