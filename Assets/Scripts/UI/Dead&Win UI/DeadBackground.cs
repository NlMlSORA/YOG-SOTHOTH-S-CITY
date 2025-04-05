using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DeadBackground : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color color;
    private float speed = 1;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }


    private void Update()
    {
        color.a += speed * 0.002f;
        sr.color = color;
    }

    public void Finished()
    {
        color.a = 0;
        sr.color = color;
        gameObject.SetActive(false);
    }


}
