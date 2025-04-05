using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorTrigger : MonoBehaviour
{
    private AudioSource audios;
    [SerializeField] private AudioClip arrowAudio;
    [SerializeField] private AudioClip comboAttackAudio;
    [SerializeField] private AudioClip dashAudio;
    //[SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip rollAttackAudio;
    [SerializeField] private AudioClip ropeAudio;
    [SerializeField] private AudioClip sweepAttackAudio;

    [SerializeField] private AudioClip upAttackAudio;




    [SerializeField] private GameObject rope;
    Enemy_Boss enemy => GetComponentInParent<Enemy_Boss>();

    private void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    private void ArrowAudio()
    {
        audios.clip = arrowAudio;
        audios.Play();
    }

    private void ComboAttackAudio()
    {
        audios.clip = comboAttackAudio;
        audios.Play();
    }
    private void DashAudio()
    {
        audios.clip = dashAudio;
        audios.Play();
    }


    private void RollAttackAudio()
    {
        audios.clip = rollAttackAudio;
        audios.Play();
    }

    private void RopeAudio()
    {
        audios.clip = ropeAudio;
        audios.Play();
    }

    private void SweepAttackAudio()
    {
        audios.clip = sweepAttackAudio;
        audios.Play();
    }

    private void UpAttackAudio()
    {
        audios.clip = upAttackAudio;
        audios.Play();
    }

    









    public void AnimationFinishTrigger() => enemy.AnimationFinishTrigger();

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                if (!PlayerManager.instance.isDead)
                {
                    float distance = enemy.transform.position.x - hit.transform.position.x;//判断敌人和玩家的左右
                    hit.GetComponent<Player>().Damage(distance);

                    PlayerStats _target = hit.GetComponent<PlayerStats>();
                    enemy.stats.DoDamage(_target);
                }
            }
        }
    }



    public void UseRope()
    {
        rope.SetActive(true);
    }

    public void DropRope()
    {
        rope.SetActive(false);
    }


    public void CreateSword()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        GameObject swordRight = Instantiate(enemy.sword, new Vector2(enemy.transform.position.x, enemy.transform.position.y-0.2f), rotation);
        GameObject swordLeft = Instantiate(enemy.sword, new Vector2(enemy.transform.position.x, enemy.transform.position.y - 0.2f), rotation);
        swordLeft.GetComponent<Sword>().Flip();
        swordLeft.GetComponent <Sword>().Move();
        swordRight.GetComponent<Sword>().Move();

    }

    public void CreateArrow()
    {
        GameObject arrow = Instantiate(enemy.arrow, enemy.transform.position, enemy.transform.rotation);
    }

    public void CreateBall()
    {
        GameObject ball_1 = Instantiate(enemy.ball, enemy.transform.position, enemy.transform.rotation);
        GameObject ball_2 = Instantiate(enemy.ball, enemy.transform.position, enemy.transform.rotation);
        GameObject ball_3 = Instantiate(enemy.ball, enemy.transform.position, enemy.transform.rotation);
        GameObject ball_4 = Instantiate(enemy.ball, enemy.transform.position, enemy.transform.rotation);
        GameObject ball_5 = Instantiate(enemy.ball, enemy.transform.position, enemy.transform.rotation);

        ball_1.GetComponent<Ball>().SetVelocity(-20, -20);
        ball_2.GetComponent<Ball>().SetVelocity(-10, -20);
        ball_3.GetComponent<Ball>().SetVelocity(0, -20);
        ball_4.GetComponent<Ball>().SetVelocity(10, -20);
        ball_5.GetComponent<Ball>().SetVelocity(20, -20);
        








    }

    public void CreateStone()
    {
        GameObject stone_1 = Instantiate(enemy.stone, enemy.stoneTransform.transform.position, enemy.transform.rotation);
        GameObject stone_2 = Instantiate(enemy.stone, enemy.stoneTransform.transform.position, enemy.transform.rotation);
        GameObject stone_3 = Instantiate(enemy.stone, enemy.stoneTransform.transform.position, enemy.transform.rotation);

        if (enemy.facingRight == true)
        {
            stone_1.GetComponent<Stone>().SetVelocity(5, 15);
            stone_2.GetComponent<Stone>().SetVelocity(7.5f, 20);
            stone_3.GetComponent<Stone>().SetVelocity(10, 25);
        }
        else
        {
            stone_1.GetComponent<Stone>().SetVelocity(-7.5f, 15);
            stone_2.GetComponent<Stone>().SetVelocity(-7.5f, 20);
            stone_3.GetComponent<Stone>().SetVelocity(-7.5f, 25);
        }



    }

    public void SweepAttackMove()
    {
        enemy.sweepAttackState.SetVelocity();
    }

    public void RollAttackTop()
    {
        enemy.rollAttackState.isTop = true;
    }



}
