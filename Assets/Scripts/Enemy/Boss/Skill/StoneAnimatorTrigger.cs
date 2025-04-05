using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneAnimatorTrigger : MonoBehaviour
{
    private AudioSource audios;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (!audios.isPlaying)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void StoneBreakAudio()
    {
        audios.Play();
    }

    private void BreakEnd()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        flag = true;
    }
}
