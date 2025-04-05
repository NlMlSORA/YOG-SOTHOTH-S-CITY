using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public int energy; // 能量值，范围为0到100
    public float smoothTime = 0.3f; // 平滑过渡的时间参数

    private float maxHeight = -0.1f;
    private float minHeight = -1.1f;
    private float targetY; // 目标 Y 坐标
    private float currentY; // 当前 Y 坐标
    private float velocity = 0f; // 用于平滑过渡的速度变量

    void Start()
    {
        targetY = CalculateYFromEnergy(energy); // 初始化目标 Y 值
        currentY = transform.localPosition.y; // 初始化当前 Y 值
    }

    void Update()
    {
        energy = SkillManager.instance.energy;
        // 如果能量值发生变化，更新目标 Y 值
        targetY = CalculateYFromEnergy(energy);

        // 使用 Mathf.SmoothDamp 实现平滑过渡
        currentY = Mathf.SmoothDamp(currentY, targetY, ref velocity, smoothTime);

        // 更新位置
        transform.localPosition = new Vector2(transform.localPosition.x, currentY);
    }

    // 根据能量值计算对应的 Y 坐标
    private float CalculateYFromEnergy(int energy)
    {
        return (maxHeight - minHeight) * energy / 100 + minHeight;
    }
}