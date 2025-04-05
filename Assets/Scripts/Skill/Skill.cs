using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Player player;
    [SerializeField] protected float cooldown;

    protected float cooldownTimer;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer <= 0 && SkillManager.instance.energy >= SkillManager.instance.energyNeed)
        {
            UseSkill();
            SkillManager.instance.ReduceEnergy();
            cooldownTimer = cooldown;
            return true;
        }

        return false;
    }

    public virtual void UseSkill()
    {
        
    }



}
