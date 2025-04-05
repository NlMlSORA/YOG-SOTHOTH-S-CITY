using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttackSkill : Skill
{
    [SerializeField] private GameObject farAttack;

    

    public override void UseSkill()
    {
        base.UseSkill();


        //CreateFarAttack();
    }

    public void CreateFarAttack()
    {
        GameObject newFarAttack = Instantiate(farAttack, player.transform.position, transform.rotation);

    }


}
