using UnityEngine;

public static class FullScreenGraphicsSettings
{
    public static void SetFullScreen(GraphicsData data, bool isFullScreen)
    {
        PlayerPrefs.SetInt(data.FullScreenKey, isFullScreen ? 1 : 0);
        PlayerPrefs.Save();

        Screen.fullScreen = isFullScreen;
        data.SetFullScreenValue(isFullScreen);
    }

    public static bool GetFullScreen(GraphicsData data)
    {
        bool savedValue = PlayerPrefs.GetInt(data.FullScreenKey, Screen.fullScreen ? 1 : 0) == 1;
        data.SetFullScreenValue(savedValue);
        return savedValue;
    }
}
