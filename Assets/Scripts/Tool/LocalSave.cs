﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存档类，统一接口可方便将存档改成txt形式
/// </summary>

#if !SAVE_TXT
public class LocalSave {
    public static void DeleteAll() {
        PlayerPrefs.DeleteAll();
    }

    public static void SaveAll() {
        PlayerPrefs.Save();
    }

    public static float GetFloat(string key, float defaultValue = 0) {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public static int GetInt(string key, int defaultValue = 0) {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static int GetInt(object obj, int defaultValue = 0) {
        return PlayerPrefs.GetInt(obj.ToString(), defaultValue);
    }

    public static string GetString(string key, string defaultValue = "") {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public static bool GetBool(string key, bool defaultValue = false) {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    public static bool HasKey(string key) {
        return PlayerPrefs.HasKey(key);
    }

    public static void SetFloat(string key, float value) {
        PlayerPrefs.SetFloat(key, value);
    }

    public static void SetInt(string key, int value) {
        PlayerPrefs.SetInt(key, value);
    }

    public static void SetInt(object obj, int value) {
        PlayerPrefs.SetInt(obj.ToString(), value);
    }

    public static void SetString(string key, string value) {
        PlayerPrefs.SetString(key, value);
    }

    public static void SetBool(string key, bool value) {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static void SetList<T>(string key, List<T> list) {
        string info = JsonUtility.ToJson(new Serialization<T>(list));
        PlayerPrefs.SetString(key, info);
    }

    public static List<T> GetList<T>(string key) {
        string info = PlayerPrefs.GetString(key, null);
        if (info == null || info == "") {
            return new List<T>();
        }
        List<T> data = JsonUtility.FromJson<Serialization<T>>(info).ToList();
        return data;
    }
}

#else
public class LocalSave {
    private static SaveData saveData = new SaveData();

    public static void DeleteAll() {
        saveData.Clear();
        Write();
    }

    public static float GetFloat(string key) {
        return GetFloat(key, 0f);
    }

    public static float GetFloat(string key, float defaultValue) {
        if (saveData.HasKey(key)) {
            return float.Parse(saveData.GetValue(key));
        }
        else {
            return defaultValue;
        }
    }

    public static int GetInt(string key) {
        return GetInt(key, 0);
    }

    public static int GetInt(string key, int defaultValue) {
        if (saveData.HasKey(key)) {
            return int.Parse(saveData.GetValue(key));
        }
        else {
            return defaultValue;
        }
    }

    public static string GetString(string key) {
        return GetString(key, "");
    }

    public static string GetString(string key, string defaultValue) {
        if (saveData.HasKey(key)) {
            return saveData.GetValue(key);
        }
        else {
            return defaultValue;
        }
    }

    public static bool HasKey(string key) {
        return saveData.HasKey(key);
    }

    public static void SetFloat(string key, float value) {
        saveData.Add(key, value.ToString());
        Write();
    }

    public static void SetInt(string key, int value) {
        saveData.Add(key, value.ToString());
        Write();
    }

    public static void SetString(string key, string value) {
        saveData.Add(key, value);
        Write();
    }

    public static void Write() {
        //LocalAssetMgr.Instance.WriteSave(saveData);
    }

    public static void Read() {
        //saveData = LocalAssetMgr.Instance.LoadSave();
    }

    public static void JustAdd(string key, string value) {
        saveData.Add(key, value);
    }
}

public class SaveData {
    public List<string> keyList = new List<string>();
    public List<string> valueList = new List<string>();

    public bool HasKey(string key) {
        for (int index = 0; index < keyList.Count; index++) {
            if (keyList[index] == key) {
                return true;
            }
        }

        return false;
    }

    public string GetValue(string key) {
        for (int index = 0; index < keyList.Count; index++) {
            if (keyList[index] == key) {
                return valueList[index];
            }
        }

        return "";
    }

    public void Add(string key, string value) {
        for (int index = 0; index < keyList.Count; index++) {
            if (keyList[index] == key) {
                valueList[index] = value;
                return;
            }
        }

        keyList.Add(key);
        valueList.Add(value);
        if (keyList.Count != valueList.Count) {
            Debug.LogError("error list error");
        }
    }

    public void Clear() {
        keyList.Clear();
        valueList.Clear();
    }
#endif

// List<T>
[Serializable]
public class Serialization<T> {
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target) {
        this.target = target;
    }
}

// Dictionary<TKey, TValue>
[Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver {
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;

    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public Serialization(Dictionary<TKey, TValue> target) {
        this.target = target;
    }

    public void OnBeforeSerialize() {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize() {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i) {
            target.Add(keys[i], values[i]);
        }
    }
}