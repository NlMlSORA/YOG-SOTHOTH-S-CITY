using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public bool isDead = false;
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    public LayerMask whatIsPlayer;

    #region Attack
    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldownTime;
    [HideInInspector] public float lastAttackTime;

    #endregion

    #region Stuned
    [Header("Stuned Info")]
    public Vector2 stunedDirection;
    public float stunedDuration;
    [SerializeField] protected GameObject counterWindow;
    protected bool canBeStuned;


    #endregion

    #region State
    public EnemyStateMachine stateMachine { get; private set; }


    #endregion


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();


    }


    protected override void Start()
    {
        base.Start();

    }


    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();


    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + attackDistance, wallCheck.position.y));
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, attackDistance, whatIsPlayer);

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    #region Stuned
    public virtual void OpenCounterWindow()
    {
        canBeStuned = true;
        counterWindow.SetActive(true);
    }

    public virtual void CloseCounterWindow()
    {
        canBeStuned = false;
        counterWindow.SetActive(false);
    }

    public virtual bool CanBeStuned()
    {
        if (canBeStuned)
        {
            CloseCounterWindow();
            return true;
        }

        return false;
    }

    #endregion

    public override void Die()
    {
        base.Die();
    }



}
