using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public int energy;
    public int energyNeed = 33;
    public DashSkill dash;
    public FarAttackSkill farAttack;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);

        dash = GetComponent<DashSkill>();
        farAttack = GetComponent<FarAttackSkill>();
    }

    public void ReduceEnergy()
    {
        energy -= 33;
    }

    public void AddEnergy()
    {
        energy += 11;
        if(energy > 100)
        {
            energy = 100;
        }
    }
}
