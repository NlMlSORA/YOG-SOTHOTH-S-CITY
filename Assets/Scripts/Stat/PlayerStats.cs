using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    Player player;

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void DoDamage(CharacterStats _targetStats)
    {
        base.DoDamage(_targetStats);
    }

    public override void TakeDamage(int damage)
    {
        if (player.canHurt)
        {
            StartCoroutine("CanHurt");
            base.TakeDamage(damage);

            if (currentHealth <= 0)
            {
                PlayerManager.instance.isDead = true;
            }
        }
    }

    public override void Die()
    {
        base.Die();
        
        player.Die();
    }

    public virtual IEnumerator CanHurt()
    {
        player.canHurt = false;
        yield return new WaitForSeconds(player.hurtDuration);
        player.canHurt = true;
    }

    public override void ResetStats()
    {
        
    }
}
