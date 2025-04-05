using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{


    #region
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunedState stunedState { get; private set; }
    public SkeletonHurtState hurtState { get; private set; }
    public SkeletonDeathState deathState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunedState = new SkeletonStunedState(this, stateMachine, "Stuned", this);
        hurtState = new SkeletonHurtState(this, stateMachine, "Hurt", this);
        deathState = new SkeletonDeathState(this, stateMachine, "Death", this);

    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Init(idleState);
        
    }

    protected override void Update()
    {
        base.Update();


    }

    public override bool CanBeStuned()
    {
        if (base.CanBeStuned())
        {
            stateMachine.ChangeState(stunedState);
            return true;
        }
        return false;
    }

    public override void Damage(float distance)
    {
        base.Damage(distance);
        stateMachine.ChangeState(hurtState);
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
}
