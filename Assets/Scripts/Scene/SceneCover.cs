using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SceneCover : MonoBehaviour
{
    public static SceneCover instance;

    private Image imgCover;
    private Color color;
    private bool active = true;
    [SerializeField] private float speed = 10;
    [SerializeField] private float countTimer;
    bool isCount = false;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        imgCover = GetComponent<Image>();
        color = imgCover.color;
        Active();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && color.a < 1)
        {
            color.a += speed * 0.002f;
            imgCover.color = color;
            if (color.a >= 1)
            {
                color.a = 1;
                countTimer = 1;
                isCount = true;
                if (Door.instance != null)
                {
                    if (Door.instance.sceneNumber == 0)
                    {
                        DestroyOnReset.instance.DestroyAll();
                    }
                }
                else if(DestroyOnReset.instance != null)
                {
                    DestroyOnReset.instance.DestroyAll();
                }
            }
        }

        else if(!active && color.a >= 0)
        {
            color.a -= speed * 0.002f;
            imgCover.color = color;
            //Debug.Log(color.a);
            if (color.a < 0)
            {
                
                gameObject.SetActive(false);

            }
        }

        if (isCount)
        {
            countTimer -= 0.002f;
            if (countTimer < 0)
            {
                InActive();
                isCount = false;
            }
        }
        

    }

    public void Active()
    {
        active = true;
        Time.timeScale = 0;
    }


    public void InActive()
    {
        active = false;

        if (Door.instance != null && Door.instance.sceneNumber > 0)
        {
            Door.instance.SetPlayerPosition();
        }
        SceneLoader.instance.asyncOperation.allowSceneActivation = true;
        Time.timeScale = 1;
    }

}
