using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    private string animBoolName;
    protected float xInput;
    protected float yInput;
    protected float stateTimer;

    public bool triggerCalled;

    protected Rigidbody2D rb;
    protected AudioSource audios;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        rb = player.rb;
        audios = player.GetComponentInChildren<AudioSource>();
        player.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        player.anim.SetFloat("yVelocity", rb.velocity.y);
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public void TriggerCalled()
    {
        triggerCalled = true;
    }
}
