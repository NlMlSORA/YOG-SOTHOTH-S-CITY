using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefTask : RefBase {

    public static Dictionary<int, RefTask> cacheMap = new Dictionary<int, RefTask>();

    public int ItemId;
    public string TaskType;
    public string Description;
    public int Count;
    public int Reward;

    public override string GetFirstKeyName() {
        return "ID";
    }

    public override void LoadByLine(Dictionary<string, string> _value, int _line) {
        base.LoadByLine(_value, _line);
        ItemId = GetInt("ID");
        TaskType = GetString("TaskType");
        Description = GetString("Description");
        Count = GetInt("Count");
        Reward = GetInt("Reward");
    }

    public static RefTask GetRef(int itemID) {
        RefTask data = null;
        if (cacheMap.TryGetValue(itemID, out data)) {
            return data;
        }

        if (data == null) {
            Debug.LogError("error RefTask key:" + itemID);
        }
        return data;
    }
}