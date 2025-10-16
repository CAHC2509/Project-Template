using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

public class CSVSaveSystem : ISaveSystem
{
    private string savePath;

    public CSVSaveSystem()
    {
        savePath = Application.persistentDataPath + "/";
    }

    public void Save(string key, object data)
    {
        string file = $"{savePath}{key}.csv";

        if (data is string str)
        {
            File.WriteAllText(file, str);
            return;
        }

        if (data is StringWrapper wrapper)
        {
            File.WriteAllText(file, wrapper.value);
            return;
        }

        if (data is IEnumerable enumerable && !(data is string))
        {
            var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext()) return;

            Type type = enumerator.Current.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(f => f.IsPublic || f.GetCustomAttribute<SerializeField>() != null)
                             .ToArray();

            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(string.Join(",", fields.Select(f => f.Name)));
                do
                {
                    object obj = enumerator.Current;
                    var values = fields.Select(f => f.GetValue(obj)?.ToString() ?? "");
                    writer.WriteLine(string.Join(",", values));
                } while (enumerator.MoveNext());
            }
        }
        else
        {
            Type type = data.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(f => f.IsPublic || f.GetCustomAttribute<SerializeField>() != null)
                             .ToArray();

            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(string.Join(",", fields.Select(f => f.Name)));
                var values = fields.Select(f => f.GetValue(data)?.ToString() ?? "");
                writer.WriteLine(string.Join(",", values));
            }
        }
    }

    public object Load(string key, Type type)
    {
        string file = $"{savePath}{key}.csv";
        if (!File.Exists(file)) return null;

        if (type == typeof(string))
            return File.ReadAllText(file);

        if (type == typeof(StringWrapper))
        {
            string value = File.ReadAllText(file);
            return new StringWrapper(value);
        }

        string[] lines = File.ReadAllLines(file);
        if (lines.Length < 2) return null;

        if (type.IsGenericType)
        {
            var elementType = type.GetGenericArguments()[0];
            var fields = elementType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                    .Where(f => f.IsPublic || f.GetCustomAttribute<SerializeField>() != null)
                                    .ToArray();

            var list = (IList)Activator.CreateInstance(type);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                ConstructorInfo ctor = elementType.GetConstructor(Type.EmptyTypes);
                object obj = ctor != null
                    ? Activator.CreateInstance(elementType)
                    : FormatterServices.GetUninitializedObject(elementType);

                for (int j = 0; j < fields.Length && j < values.Length; j++)
                {
                    if (string.IsNullOrEmpty(values[j])) continue;
                    object value = Convert.ChangeType(values[j], fields[j].FieldType);
                    fields[j].SetValue(obj, value);
                }

                list.Add(obj);
            }

            return list;
        }
        else
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(f => f.IsPublic || f.GetCustomAttribute<SerializeField>() != null)
                             .ToArray();

            string[] values = lines[1].Split(',');

            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            object obj = ctor != null
                ? Activator.CreateInstance(type)
                : FormatterServices.GetUninitializedObject(type);

            for (int j = 0; j < fields.Length && j < values.Length; j++)
            {
                if (string.IsNullOrEmpty(values[j])) continue;
                object value = Convert.ChangeType(values[j], fields[j].FieldType);
                fields[j].SetValue(obj, value);
            }

            return obj;
        }
    }

    public bool HasKey(string key)
    {
        return File.Exists($"{savePath}{key}.csv");
    }

    public void Delete(string key)
    {
        string file = $"{savePath}{key}.csv";
        if (File.Exists(file)) File.Delete(file);
    }
}
