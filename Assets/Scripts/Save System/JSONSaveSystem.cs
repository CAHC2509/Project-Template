using System;
using System.IO;
using UnityEngine;

public class JSONSaveSystem : ISaveSystem
{
    private string savePath;

    public JSONSaveSystem()
    {
        savePath = Application.persistentDataPath + "/";
    }

    public void Save(string key, object data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText($"{savePath}{key}.json", json);
    }

    public object Load(string key, Type type)
    {
        string file = $"{savePath}{key}.json";
        if (!File.Exists(file)) return Activator.CreateInstance(type);
        string json = File.ReadAllText(file);
        return JsonUtility.FromJson(json, type);
    }

    public bool HasKey(string key)
    {
        return File.Exists($"{savePath}{key}.json");
    }

    public void Delete(string key)
    {
        string file = $"{savePath}{key}.json";
        if (File.Exists(file)) File.Delete(file);
    }
}
