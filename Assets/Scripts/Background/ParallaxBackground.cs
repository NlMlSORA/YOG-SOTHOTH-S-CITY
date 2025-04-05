using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;
    [SerializeField] private float moveSpeed;

    private float length;
    private float xPosition;

    private Vector3 lastCamPosition; // 用于存储上一帧摄像机的位置


    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        lastCamPosition = cam.transform.position;
    }


    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);


        // 添加自动向右移动的逻辑
        
        float deltaTime = Time.deltaTime; // 获取自上一帧以来的时间

        // 更新xPosition，包括自动向右移动的部分
        xPosition += moveSpeed * deltaTime;

        

        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }


        transform.position = new Vector2(xPosition + distanceToMove, transform.position.y);
    }
}
