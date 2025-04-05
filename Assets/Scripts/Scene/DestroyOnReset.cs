using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnReset : MonoBehaviour
{
    public static DestroyOnReset instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyAll()
    {
        Destroy(gameObject);
    }
}
