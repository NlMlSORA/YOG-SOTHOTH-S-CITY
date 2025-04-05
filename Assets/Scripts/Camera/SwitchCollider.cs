using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchCollider : MonoBehaviour
{
    public static SwitchCollider instance;

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
        //DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        //Invoke("SwitchConfinerShape", 5);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SwitchMainConfinerShape();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMainConfinerShape()
    {
        PolygonCollider2D confinerShape = GameObject.Find("MapCollider").GetComponent<PolygonCollider2D>();

        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();

        confiner.m_BoundingShape2D = confinerShape;


    }

    void OnDestroy()
    {
        // 确保在对象销毁时清理所有资源
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
