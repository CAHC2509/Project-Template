using System;

public interface ISaveSystem
{
    public void Save(string key, object data);
    public object Load(string key, Type type);
    public bool HasKey(string key);
    public void Delete(string key);
}
