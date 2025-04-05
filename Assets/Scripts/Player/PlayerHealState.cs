using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerGroundedState
{
    bool flag = false;
    ParticleSystem ps;

    public PlayerHealState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        HPManager.instance.AddHP();
        flag = false;
        ps = player.gameObject.GetComponentInChildren<ParticleSystem>(true);
        ps.gameObject.SetActive(true);
        ps.Play();
        Time.timeScale = 0.7f;
        UIEffectMgr.Instance.ShowHealOverlayUI();
    }

    public override void Exit()
    {
        base.Exit();
        ps.gameObject.SetActive(false);
        if (!flag)
        {
            HPManager.instance.InterruptHeal();
        }
        Time.timeScale = 1f;
    }

    public override void Update()
    {
        base.Update();
        
        if (Input.GetKeyUp(KeyCode.O))
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        player.SetZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }

    public void HealSuccess()
    {
        flag = true;
        player.stats.currentHealth += 1;
        SkillManager.instance.ReduceEnergy();
    }

}
