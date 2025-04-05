using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sella : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void First()
    {
        anim.SetBool("First", false);
        anim.SetBool("Second", true);
    }

    public void Second()
    {
        anim.SetBool("Second", false);
        anim.SetBool("First", true);
    }
}
