using UnityEngine;

public class GraphicsResolutionSettings : MonoBehaviour
{
    public static void SaveResolution(GraphicsData data, Resolution resolution)
    {
        PlayerPrefs.SetInt(data.ResolutionWidthKey, resolution.width);
        PlayerPrefs.SetInt(data.ResolutionHeightKey, resolution.height);
        PlayerPrefs.Save();
    }

    public static Resolution GetSavedResolution(GraphicsData data)
    {
        int width = PlayerPrefs.GetInt(data.ResolutionWidthKey, Screen.currentResolution.width);
        int height = PlayerPrefs.GetInt(data.ResolutionHeightKey, Screen.currentResolution.height);
        return new Resolution { width = width, height = height };
    }
}
