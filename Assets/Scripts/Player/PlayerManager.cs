using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;
    public bool isDead = false;
    [SerializeField] private GameObject deadFont;

    public bool isIdle;


    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;

    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void DeadFontDisplay()
    {
        deadFont.GetComponent<DeadFont>().Display();
    }



}
