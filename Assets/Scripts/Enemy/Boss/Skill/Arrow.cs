using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : BossSkillItem
{
    [SerializeField] private float speed;
    

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        if (transform.rotation.y > 0)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }


    }

    // Update is called once per frame
    protected override void Update()
    {
        base .Update();
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

        }
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
