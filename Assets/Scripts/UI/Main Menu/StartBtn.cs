using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtnPress()
    {
        SceneLoader.instance.LoadScene("CityScene");
        transform.parent.gameObject.SetActive(false);
    }
}
