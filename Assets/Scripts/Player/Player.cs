using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    [HideInInspector] public float dashDir;
    public GameObject dashGameObject;

    [Header("Attack Info")]
    public Vector2[] attackMovement;
    public bool isBusy { get; private set; }
    public Transform attackUpCheck;
    public float attackUpCheckRadius;
    public Transform attackDownCheck;
    public float attackDownCheckRadius;

    [Header("CounterAttack Info")]
    public float counterAttackDuration;

    [Header("Hurt Info")]
    public float hurtDuration;
    [SerializeField] private GameObject getHurtEffect;

    [Header("Audio Info")]
    public float normalVolume;
    public float minVolume;

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerHurtState hurtState { get; private set; }
    public PlayerDeathState deathState { get; private set; }
    public PlayerAttackUpState attackUpState { get; private set; }
    public PlayerAttackDownState attackDownState { get; private set; }

    //public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerAirAttackState airAttackState { get; private set; }
    public PlayerFarAttackState farAttackState { get; private set; }
    //public PlayerCounterAttackState counterAttack { get; private set; }
    //public PlayerAimState aimState { get; private set; }
    //public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerHealState healState { get; private set; }


    #endregion

    private Vector2 startPosition;
    public float gravity;

    public bool isIdle;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        runState = new PlayerRunState(this, stateMachine, "Run");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        fallState = new PlayerFallState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        hurtState = new PlayerHurtState(this, stateMachine, "Hurt");
        deathState = new PlayerDeathState(this, stateMachine, "Death");
        attackUpState = new PlayerAttackUpState(this, stateMachine, "AttackUp");
        attackDownState = new PlayerAttackDownState(this, stateMachine, "AttackDown");
        //wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        airAttackState = new PlayerAirAttackState(this, stateMachine, "AirAttack");
        farAttackState = new PlayerFarAttackState(this, stateMachine, "FarAttack");
        //counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        //aimState = new PlayerAimState(this, stateMachine, "AimSword");
        //catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        healState = new PlayerHealState(this, stateMachine, "Heal");


        startPosition = transform.position;
    }

    protected override void Start()
    {
        base.Start();
        gravity = rb.gravityScale;
        stateMachine.InitState(idleState);


        //DontDestroyOnLoad(gameObject);
    }

    protected override void Update()
    {
        if (!isIdle)
        {
            base.Update();
            stateMachine.currentState.Update();
            CheckForDashInput();
        }
        else
        {
            stateMachine.ChangeState(idleState);
            SetZeroVelocity();
        }
    }

    public void CheckForDashInput()
    {

        
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (SkillManager.instance.dash.CanUseSkill() && PlayerManager.instance.isDead == false)
            {
                dashDir = Input.GetAxisRaw("Horizontal");
                if (dashDir == 0)
                {
                    dashDir = facingDir;
                }

                stateMachine.ChangeState(dashState);
            }

        }
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void TriggerCalled() => stateMachine.currentState.TriggerCalled();

    public override void Damage(float distance)
    {
        if (canHurt)
        {
            base.Damage(distance);
            GetComponentInChildren<PlayerAnimations>().HurtAudio();
            HPManager.instance.ReduceHP();
            stateMachine.ChangeState(hurtState);
            Instantiate(getHurtEffect, transform.position, transform.rotation, transform);
        }
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackUpCheck.position, attackUpCheckRadius);
        Gizmos.DrawWireSphere(attackDownCheck.position, attackDownCheckRadius);
    }

    public void ResetPlayer()
    {
        PlayerManager.instance.isDead = false;
        SkillManager.instance.energy = 0;
        stats.ResetStats();
        stateMachine.ChangeState(idleState);
        transform.position = startPosition;
    }
}
