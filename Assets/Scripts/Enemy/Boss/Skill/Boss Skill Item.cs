using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillItem : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Enemy_Boss enemy;
    protected Player player;
    protected Collider2D targetCollider;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        targetCollider = gameObject.GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemy = BossManager.instance.boss;
        player = PlayerManager.instance.player;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }


}
