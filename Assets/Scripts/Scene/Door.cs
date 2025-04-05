using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public static Door instance;
    public int sceneNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (sceneNumber == 1)
            {
                SceneLoader.instance.LoadScene("WildScene");
                SwitchCollider.instance.SwitchMainConfinerShape();
            }

            else if (sceneNumber == 2)
            {
                SceneLoader.instance.LoadScene("ForestScene");
                SwitchCollider.instance.SwitchMainConfinerShape();
            }
        }
    }

    public void SetPlayerPosition()
    {
        //if (sceneNumber == 0)
        //{
        //    PlayerManager.instance.player.transform.position = new Vector2(-26.6f, -5.9f);
        //}
        if (sceneNumber == 1)
        {
            PlayerManager.instance.player.transform.position = new Vector2(-30.54f, -3.58f);
            PetView.Instance.TeleportBehindPlayer();
        }
        else if(sceneNumber == 2)
        {
            PlayerManager.instance.player.transform.position = new Vector2(-2, -3);
            PetView.Instance.TeleportBehindPlayer();
        }
    }
}
