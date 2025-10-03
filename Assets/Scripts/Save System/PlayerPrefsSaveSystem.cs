using System;
using UnityEngine;

public class PlayerPrefsSaveSystem : ISaveSystem
{
    public void Save(string key, object data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public object Load(string key, Type type)
    {
        string data = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson(data, type);
    }

    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public void Delete(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }
}
