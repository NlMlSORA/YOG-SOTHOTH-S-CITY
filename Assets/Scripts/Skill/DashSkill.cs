using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill
{
    public override bool CanUseSkill()
    {
        if (cooldownTimer <= 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }

        return false;
    }


}
