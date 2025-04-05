using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BossSkillItem
{
    [SerializeField] private float speed;
    private bool isFlip = false;
    

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

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFlip = true;
    }

    public void Move()
    {
        if (!isFlip)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Invoke("DestroyItem", 4);
        }

        if (collision.tag == "Player")
        {
            float distance = enemy.transform.position.x - player.transform.position.x;//判断敌人和玩家的左右
            collision.GetComponent<Player>().Damage(distance);
            targetCollider.enabled = false;
            BossManager.instance.boss.stats.DoDamage(collision.GetComponent<Player>().stats);

            

            //anim.SetBool("ArrowBreak", true);
            //rb.velocity = Vector2.zero;
        }
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
