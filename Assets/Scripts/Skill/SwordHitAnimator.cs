using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitAnimator : MonoBehaviour
{
    public void TriggerCalled()
    {
        Destroy(transform.parent.gameObject);
    }

    public void SwordHitAudio()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
