using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefShop : RefBase {

    public static Dictionary<int, RefShop> cacheMap = new Dictionary<int, RefShop>();

    public int ItemId;
    public string IconPath;
    public string Title;
    public string Description;
    public int Cost;

    public override string GetFirstKeyName() {
        return "ItemId";
    }

    public override void LoadByLine(Dictionary<string, string> _value, int _line) {
        base.LoadByLine(_value, _line);
        ItemId = GetInt("ItemId");
        IconPath = GetString("IconPath");
        Title = GetString("Title");
        Description = GetString("Description");
        Cost = GetInt("Cost");
    }

    public static RefShop GetRef(int itemID) {
        RefShop data = null;
        if (cacheMap.TryGetValue(itemID, out data)) {
            return data;
        }

        if (data == null) {
            Debug.LogError("error RefShop key:" + itemID);
        }
        return data;
    }
}