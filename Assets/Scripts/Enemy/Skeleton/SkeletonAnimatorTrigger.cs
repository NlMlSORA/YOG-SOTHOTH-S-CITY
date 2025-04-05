using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimatorTrigger : MonoBehaviour
{
    Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    public void AnimationFinishTrigger() => enemy.AnimationFinishTrigger();

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                float distance = enemy.transform.position.x - hit.transform.position.x;//判断敌人和玩家的左右
                hit.GetComponent<Player>().Damage(distance);

                PlayerStats _target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(_target);
            }
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterWindow();
    private void CloseCounterWindow() => enemy.CloseCounterWindow();
}
