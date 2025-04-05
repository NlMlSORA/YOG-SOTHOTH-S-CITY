using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy
{
    [Header("Up Info")]
    [SerializeField] private Transform upCheck;
    [SerializeField] private float upCheckDistance;
    public float upSpeed;
    public bool canUp = false;
    public Transform bossMiddlePosition;

    [Header("Down Info")]
    public float downSpeed;

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    [HideInInspector] public float dashDir;

    [Header("Hide Info")]
    public float hideTimer;
    public float middleDistance; //生成位置与中心点的距离

    [Header("Skill GameObjects")]
    public GameObject arrow;
    public GameObject ball;
    public GameObject stone;
    public GameObject sword;

    public GameObject stoneTransform;

    #region state
    public BossIdleState idleState {  get; private set; }
    public BossDashState dashState { get; private set; }
    public BossComboAttackState comboAttackState { get; private set; }
    public BossRollAttackState rollAttackState { get; private set; }
    public BossSweepAttackState sweepAttackState { get; private set; }
    public BossUpState upState { get; private set; }
    public BossUpAttackState upAttackState { get; private set; }
    public BossDownState downState { get; private set; }
    public BossDownAttackState downAttackState { get; private set; }
    public BossHideState hideState { get; private set; }
    public BossDeathState deathState { get; private set; }

    #endregion

    [HideInInspector] public float gravity;
    [HideInInspector] private bool dashed;
    [HideInInspector] private Player player;
    private int lastA = -1;
    private int lastB = -1;
    


    protected override void Awake()
    {
        base.Awake();

        idleState = new BossIdleState(this, stateMachine, "Idle", this);
        dashState = new BossDashState(this, stateMachine, "Dash", this);
        comboAttackState = new BossComboAttackState(this, stateMachine, "ComboAttack", this);
        rollAttackState = new BossRollAttackState(this, stateMachine, "RollAttack", this);
        sweepAttackState = new BossSweepAttackState(this, stateMachine, "SweepAttack", this);
        upState = new BossUpState(this, stateMachine, "Up", this);
        upAttackState = new BossUpAttackState(this, stateMachine, "UpAttack", this);
        downState = new BossDownState(this, stateMachine, "Down", this);
        downAttackState = new BossDownAttackState(this, stateMachine, "DownAttack", this);
        hideState = new BossHideState(this, stateMachine, "Hide", this);
        deathState = new BossDeathState(this, stateMachine, "Death", this);
    }

    protected override void Start()
    {
        base.Start();
        gravity = rb.gravityScale;
        player = PlayerManager.instance.player;
        stateMachine.Init(idleState);
    }

    protected override void Update()
    {
        base.Update();


    }

    public void RandomAttack()
    {
        int a = Random.Range(0, 2);

        if (a == lastA) { 
            RandomAttack();
            return;
        }
        lastA = a;

        //if (a == 0)
        //{
        //    stateMachine.ChangeState(sweepAttackState);
        //    return;
        //}

        if (a == 0)
        {
            if (GetPlayerDistance() < 3)
            {
                stateMachine.ChangeState(comboAttackState);
                return;
            }

            else
            {
                int b = Random.Range(0, 2);

                if (b == lastB)
                {
                    if (b == 0)
                    {
                        b = 1;
                    }

                    if (b == 1)
                    {
                        b = 0;
                    }
                }
                lastB = b;

                if (b == 0)
                {
                    stateMachine.ChangeState(rollAttackState);
                    return;
                }
                else
                {
                    stateMachine.ChangeState(upState);
                    return;
                }
            }
        }

        else
        {
            stateMachine.ChangeState(dashState);
            return;
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(upCheck.position, new Vector2(upCheck.position.x, upCheck.position.y + upCheckDistance));
    }

    public virtual bool isUpDetected() => Physics2D.Raycast(upCheck.position, Vector2.up, upCheckDistance, whatIsGround);

    public override void Damage(float distance)
    {
        
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);

        WinFont.instance.Display();
    }

    private float GetPlayerDistance()
    {
        if (player.transform.position.x > transform.position.x)
        {
            return player.transform.position.x - transform.position.x;
        }
        else
        {
            return transform.position.x - player.transform.position.x;
        }
    }

    public void FaceToPlayer()
    {
        float distance = transform.position.x - player.transform.position.x;
        if (facingRight == true && distance > 0)
        {
            Flip();
        }

        if (facingRight == false && distance < 0)
        {
            Flip();
        }
    }
}
