using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : SingletonMonoBehavior<TaskUI>
{
    private GameObject imgLightGO;    //贩卖机边缘发光
    public TaskInfo TaskInfo;

    [SerializeField] private float distance;

    protected override void Awake()
    {
        imgLightGO = gameObject.GetChildControl<SpriteRenderer>("imgLight").gameObject;

        
        


    }

    private void Update()
    {
        if (PlayerManager.instance.player != null)
        {
            distance = Mathf.Abs(PlayerManager.instance.player.transform.position.x - transform.position.x);
            if (distance <= 2 && !imgLightGO.activeSelf)
            {
                imgLightGO.SetActive(true);
            }
            else if (distance > 2 && imgLightGO.activeSelf)
            {
                imgLightGO.SetActive(false);
            }

            if (distance <= 2)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    TaskWindow.Instance.ChangeWindowState();
                }
            }
        }
    }
}
