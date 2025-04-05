using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHealth;
    public int currentHealth;

    public System.Action OnHealthChanged;

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {


        int totalDamage = damage.GetValue();

        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(OnHealthChanged != null)
        {
            OnHealthChanged();
        }


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        
    }

    public virtual void ResetStats()
    {
        currentHealth = maxHealth.GetValue();
    }
}

