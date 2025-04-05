using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Enemy enemy;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(int damage)
    {
        if (enemy.canHurt)
        {
            base.TakeDamage(damage);
        }

        
    }

    public override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
