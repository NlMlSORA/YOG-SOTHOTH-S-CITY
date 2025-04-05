using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinFont : MonoBehaviour
{
    public static WinFont instance;

    private TextMeshProUGUI text;
    bool display = false;
    Color color;
    [SerializeField] private float speed;
    [SerializeField] private GameObject winBackground;
    [SerializeField] private GameObject btn;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        color = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (display && text.color.a < 1)
        {
            color.a += speed * 0.002f;
            text.color = color;
            if (color.a >= 1)
            {
                //显示按钮
                btn.SetActive(true);



            }
        }
    }

    public void Display()
    {
        display = true;
        winBackground.SetActive(true);
    }
}
