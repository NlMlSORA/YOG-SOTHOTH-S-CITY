using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskView
{
    public GameObject ItemGO;
    private Image imgOutLine;
    private TextMeshProUGUI textDesc;
    private TextMeshProUGUI textType;
    private TextMeshProUGUI textState;
    private TextMeshProUGUI textCoin;
    private Button btnTask;

    public TaskInfo TaskInfo;

    public TaskView(GameObject _itemGO)
    {
        ItemGO = _itemGO;
        imgOutLine = ItemGO.GetChildControl<Image>("imgOutLine");
        textDesc = ItemGO.GetChildControl<TextMeshProUGUI>("textDesc");
        textType = ItemGO.GetChildControl<TextMeshProUGUI>("textType");
        textState = ItemGO.GetChildControl<TextMeshProUGUI>("textState");
        textCoin = ItemGO.GetChildControl<TextMeshProUGUI>("textReward/textCoin");
        btnTask = ItemGO.GetChildControl<Button>("btnTask");
        btnTask.onClick.AddListener(BtnTaskClick);
    }

    public void SetData(TaskInfo _taskInfo)
    {
        TaskInfo = _taskInfo;
        Refresh();
    }

    public void Refresh()
    {
        if (InGameTaskView.Instance.currentTaskInfo != null)
        {
            imgOutLine.gameObject.SetActive(InGameTaskView.Instance.currentTaskInfo.ID == TaskInfo.ID);
        }
        else
        {
            imgOutLine.gameObject.SetActive(false);
        }
        
        textDesc.text = TaskInfo.Description;
        textType.text = "任务类型： " + TaskInfo.TaskType;
        textState.text = TaskInfo.isCompelete ? "完成" : "未完成";
        textState.color = TaskInfo.isCompelete ? Color.green : Color.red;
        textCoin.text = TaskInfo.Reward.ToString();

    }

    public void BtnTaskClick()
    {
        Send.SendMsg(SendType.UpdateTextTask, TaskInfo);
        TaskWindow.Instance.Refresh();
    }
}
