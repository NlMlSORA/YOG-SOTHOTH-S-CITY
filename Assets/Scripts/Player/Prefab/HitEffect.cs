using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private float stayTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AnimOver()
    {
        Invoke("DestroyHitEffect", stayTimer);
    }

    private void DestroyHitEffect()
    {
        Destroy(transform.parent.gameObject);
    }
}
