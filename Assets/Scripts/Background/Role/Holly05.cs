using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holly05 : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private bool isWalking = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            rb.velocity = new Vector2(1, 0);
        }
        

        if(transform.position.x >= -12.26 && isWalking == true)
        {
            isWalking = false;
            rb.velocity = Vector2.zero;
            anim.SetBool("Stand", true);
        }
    }

    public void CloseEyes()
    {
        Invoke("Close", 2);
    }

    public void OpenEyes()
    {
        anim.SetBool("Open", true);
        anim.SetBool("Close", false);
    }

    private void Close()
    {
        anim.SetBool("Stand", false);
        anim.SetBool("Close", true);
        anim.SetBool("Open", false);
    }

}
