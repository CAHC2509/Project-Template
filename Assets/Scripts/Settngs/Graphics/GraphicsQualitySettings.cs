using UnityEngine;

public static class GraphicsQualitySettings
{
    public static int GetQualityLevel(GraphicsData data) => PlayerPrefs.GetInt(data.QualityKey, UnityEngine.QualitySettings.GetQualityLevel());
    public static void SetQualityLevel(GraphicsData data, int index) => PlayerPrefs.SetInt(data.QualityKey, index);
    public static void Save() => PlayerPrefs.Save();
}
