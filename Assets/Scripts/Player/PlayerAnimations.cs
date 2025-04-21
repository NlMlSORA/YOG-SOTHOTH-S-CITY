using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    [SerializeField] private GameObject hitEffect;

    private AudioSource audios;
    [SerializeField] private AudioSource skillAudios;

    [SerializeField] private AudioClip attackAudio;
    
    [SerializeField] private AudioClip dashAudio;
    
    [SerializeField] private AudioClip farAttackAudio;
    [SerializeField] private AudioClip healAudio;
    [SerializeField] private AudioClip hurtAudio;
    [SerializeField] private AudioClip runAudio;

    public List<AudioSource> EffectsAudioList = new();

    private void TriggerCalled() => player.TriggerCalled();

    private void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    public void RunAudio()
    {
        if (!audios.isPlaying)
        {
            audios.clip = runAudio;
            audios.Play();
        }
        
    }

    private void DashAudio()
    {
        PlayAudio(dashAudio);
    }


    private void AttackAudio()
    {
        PlayAudio(attackAudio);
    }

    private void FarAttackAudio()
    {
        PlayAudio(farAttackAudio);
    }

    private void HealAudio()
    {
        PlayAudio(healAudio);
    }

    public void HurtAudio()
    {
        PlayAudio(hurtAudio);
    }

    private void PlayAudio(AudioClip _clip)
    {
        // 创建一个空的游戏对象
        GameObject audioObject = ObjectPool.Instance.Get("EffectsAudio", GameObject.Find("DontDestroy").transform ,false);

        // 为游戏对象添加 AudioSource 组件
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        // 设置 AudioSource 的属性
        audioSource.clip = _clip;
        audioSource.volume = SettingWindow.Instance.GetEffectsAudioValue(); // 设置音量（范围：0-1）
        audioSource.pitch = 1.0f; // 设置音高（默认值为1）
        audioSource.loop = false; // 是否循环播放
        audioSource.playOnAwake = false; // 是否在 Awake 时自动播放

        // 播放音频
        audioSource.Play();

        StartCoroutine(RecycleAfterPlay(audioSource, audioObject));
    }


    private IEnumerator RecycleAfterPlay(AudioSource source, GameObject obj)
    {
        yield return new WaitForSeconds(source.clip.length / source.pitch); // 考虑 pitch 影响播放时间
        ObjectPool.Instance.Recycle(obj);
    }




    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && hit.GetComponent<Enemy>().isDead == false)
            {
                float distance = player.transform.position.x - hit.transform.position.x;//判断敌人和玩家的左右

                hit.GetComponent<Enemy>().Damage(distance);

                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(_target);

                SkillManager.instance.AddEnergy();

                Instantiate(hitEffect, hit.transform.position, transform.rotation);
            }
        }
    }

    private void AttackUpTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackUpCheck.position, player.attackUpCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && hit.GetComponent<Enemy>().isDead == false)
            {
                float distance = player.transform.position.x - hit.transform.position.x;//判断敌人和玩家的左右

                hit.GetComponent<Enemy>().Damage(distance);

                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(_target);

                SkillManager.instance.AddEnergy();

                Instantiate(hitEffect, hit.transform.position, transform.rotation);
            }
        }
    }

    private void AttackDownTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackDownCheck.position, player.attackDownCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && hit.GetComponent<Enemy>().isDead == false)
            {
                float distance = player.transform.position.x - hit.transform.position.x;//判断敌人和玩家的左右

                hit.GetComponent<Enemy>().Damage(distance);

                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(_target);

                player.SetVelocity(player.rb.velocity.x, 15);

                SkillManager.instance.AddEnergy();

                Instantiate(hitEffect, hit.transform.position, transform.rotation);
            }
        }
    }

    private void CreateFarAttack()
    {
        player.farAttackState.StartFarAttack();
        SkillManager.instance.farAttack.CreateFarAttack();
    }

    private void HealSuccess() => player.healState.HealSuccess();

    //private void ThrowSword()
    //{
        //SkillManager.instance.swordSkill.CreateSword();
    //}
}
