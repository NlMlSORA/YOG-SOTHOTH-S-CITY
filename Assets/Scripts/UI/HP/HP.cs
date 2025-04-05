using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Start", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHP()
    {
        anim.SetBool("NHP", false);
        anim.SetBool("Reduce", false);
        anim.SetBool("Add", true);
    }

    public void ReduceHP()
    {
        anim.SetBool("Add", false);
        anim.SetBool("YHP", false);
        anim.SetBool("HP", false);
        anim.SetBool("Reduce", true);
    }

    private void StartEnd()
    {
        HPManager.instance.IsCreatedHP();
    }

    public void HPStart()
    {
        anim.SetBool("Start", false);
        anim.SetBool("YHP", true);
    }
    

    private void AddEnd()
    {
        anim.SetBool("Add", false);
        anim.SetBool("YHP", true);
    }

    private void ReduceEnd()
    {
        anim.SetBool("Reduce", false);
        anim.SetBool("NHP", true);
    }

    public void InterruptHeal()
    {
        anim.SetBool("Add", false);
        anim.SetBool("NHP", true);
    }

    public void BlingBling()
    {
        anim.SetBool("YHP", false);
        anim.SetBool("HP", true);
    }

    private void BlingOver()
    {
        anim.SetBool("YHP", true);
        anim.SetBool("HP", false);
    }
}
