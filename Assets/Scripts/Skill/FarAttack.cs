using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttack : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] GameObject swordHit;
    [SerializeField] private float speed;
    private float attackDir;
    private Rigidbody2D rb;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerManager.instance.player;
        attackDir = player.facingDir;
        transform.position = player.transform.position;
        if( attackDir == -1)
        {
            transform.Rotate(0, 180, 0);
            rb.velocity = Vector3.left * speed;
            distance = -distance;
        }
        else
        {
            rb.velocity = Vector3.right * speed;
        }

        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的对象是否为特定的游戏对象（比如敌人）
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject newSwordHit = Instantiate(swordHit, collision.transform.position, transform.rotation);

            float distance = transform.position.x - collision.transform.position.x;
            collision.gameObject.GetComponent<Enemy>().Damage(distance);
            PlayerManager.instance.player.stats.DoDamage(collision.gameObject.GetComponent<EnemyStats>());

            newSwordHit.GetComponentInChildren<SwordHitAnimator>().SwordHitAudio();
        }
        

        if (collision.gameObject.tag == "Ground")
        {
            GameObject newSwordHit = Instantiate(swordHit, new Vector2(transform.position.x + distance, transform.position.y), transform.rotation);
            Destroy(gameObject);
        }
    }
}
