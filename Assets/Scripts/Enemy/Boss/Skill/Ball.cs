using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Ball : BossSkillItem
{
    

    protected override void Awake()
    {
        base.Awake();
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void SetVelocity(float x_velocity, float y_velocity)
    {
        rb.velocity = new Vector2 (x_velocity, y_velocity);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            anim.SetBool("BallBreak", true);
            SetVelocity(0, 0);
        }

        if (collision.tag == "Player")
        {
            float distance = enemy.transform.position.x - player.transform.position.x;//判断敌人和玩家的左右
            collision.GetComponent<Player>().Damage(distance);
            targetCollider.enabled = false;
            BossManager.instance.boss.stats.DoDamage(collision.GetComponent<Player>().stats);

            anim.SetBool("BallBreak", true);
            SetVelocity(0, 0);
        }
    }

}
