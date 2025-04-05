using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimatorTrigger : MonoBehaviour
{
    Ball ball => GetComponentInParent<Ball>();

    private AudioSource audios;

    private void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    public void BreakEnd()
    {
        Destroy(ball.gameObject);
    }

    private void BreakAudio()
    {
        audios.Play();
    }
}
