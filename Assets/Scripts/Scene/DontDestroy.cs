using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
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

    private void OnApplicationQuit()
    {
        LocalSave.SaveAll();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            LocalSave.SaveAll();
        }
    }
}
