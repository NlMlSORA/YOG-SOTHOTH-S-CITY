using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    //private ParticleSystem.TextureSheetAnimationModule textureSheetAnim;

    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;

        AfterImageEffect.instance.StartAfterImageEffect();

        //player.dashGameObject.SetActive(true);
        //var renderer = player.dashGameObject.GetComponent<ParticleSystemRenderer>();
        //if (player.facingRight)
        //{
        //    renderer.flip = new Vector3(1, 0, 0);
        //}
        //else
        //{
        //    renderer.flip = new Vector3(0, 0, 0);
        //}
        player.rb.gravityScale = 0;
        
        //SkillManager.instance.cloneSkill.CreateClone(player.transform);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        //player.dashGameObject.SetActive(false);
        rb.gravityScale = player.gravity;

        AfterImageEffect.instance.EndAfterImageEffect();
    }

    public override void Update()
    {
        base.Update();

        //if (player.IsWallDetected() && !player.IsGroundDetected())
        //{
        //    stateMachine.ChangeState(player.wallSlideState);
        //}

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
            if (stateTimer < -0.15)
            {
                if (player.IsGroundDetected())
                {
                    stateMachine.ChangeState(player.idleState);
                }
                else
                {
                    stateMachine.ChangeState(player.fallState);
                }
            }
        }
    }
}
