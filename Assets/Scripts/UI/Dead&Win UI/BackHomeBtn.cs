using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BackHomeBtn : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;

    Color imageColor;
    Color textColor;

    float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        imageColor = image.color;
        textColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        imageColor.a += speed * 0.002f;
        textColor.a += speed * 0.002f;
        image.color = imageColor;
        text.color = textColor;
    }



    public void OnDestroy()
    {
        imageColor.a = 0;
        textColor.a = 0;
        image.color = imageColor;
        text.color = textColor;
    }



    public void BackHomeBtnPress()
    {
        Door.instance.sceneNumber = 0;
        SceneLoader.instance.LoadScene("MenuScene");

    }
}
