using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameTaskView : SingletonMonoBehavior<InGameTaskView>
{
    private TextMeshProUGUI textTask;
    public TaskInfo currentTaskInfo;
    private int count;
    public int Count
    {
        get => count;
        set {
            count = value;
            LocalSave.SetInt("KillSkeleton", count);
            Send.SendMsg(SendType.KillSkeleton, count);
            Send.SendMsg(SendType.UpdateTextTask, currentTaskInfo);

        }
    }
    //public List<TaskView> taskViewList = new();

    // Start is called before the first frame update
    void Start()
    {
        textTask = GetComponent<TextMeshProUGUI>();
        int a = LocalSave.GetInt("CurrentTaskInfoID", 0);
        if (a != 0)
        {
            currentTaskInfo = TaskWindow.Instance.taskViewList[a-1].TaskInfo;
        }
        Send.RegisterMsg(SendType.UpdateTextTask, UpdateTextTask);
        Count = LocalSave.GetInt("KillSkeleton", 0);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTextTask(object[] _objs)
    {
        currentTaskInfo = (TaskInfo)_objs[0];
        if (currentTaskInfo == null)
        {
            textTask.text = "";
            return;
        }

        LocalSave.SetInt("CurrentTaskInfoID", currentTaskInfo.ID);
        if (currentTaskInfo.Count > Count)
        {
            textTask.text = "当前任务：\n" + currentTaskInfo.Description + "\n(" + Count + "/" + currentTaskInfo.Count + ")";
        }
        else
        {
            textTask.text = "当前任务：\n" + currentTaskInfo.Description + "\n已完成";
        }
    }

    public void OnDestroy()
    {
        Send.UnregisterMsg(SendType.UpdateTextTask, UpdateTextTask);
    }
}
