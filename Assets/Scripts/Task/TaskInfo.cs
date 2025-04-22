using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInfo
{
    public const string Task = "TASK";
    public int ID;
    public string TaskType;
    public string Description;
    public int Count;
    public int Reward;
    public bool isCompelete;

    RefTask refTask;

    public TaskInfo(int _id)
    {
        ID = _id;
        refTask = RefTask.GetRef(_id);
        TaskType = refTask.TaskType;
        Description = refTask.Description;
        Count = refTask.Count;
        Reward = refTask.Reward;
        isCompelete = LocalSave.GetBool(Task + ID, isCompelete);
    }

    public void Init()
    {
        Send.RegisterMsg(SendType.KillSkeleton, IsFinish);
    }

    public void Clear()
    {
        Send.UnregisterMsg(SendType.KillSkeleton, IsFinish);
    }

    public void IsFinish(object[] _objs)
    {
        if (isCompelete)
        {
        }
        else
        {
            if ((int)_objs[0] >= Count)
            {
                //Send.SendMsg(SendType.MissionComplete, this);
                isCompelete = true;
                LocalSave.SetBool(Task + ID, isCompelete);
            }
        }
        TaskWindow.Instance.Refresh();
    }
}
