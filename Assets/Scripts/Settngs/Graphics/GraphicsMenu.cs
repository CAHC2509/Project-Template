using UnityEngine;

public class GraphicsMenu : SettingsSubMenuBase
{
    [SerializeField] private GraphicsData graphicsData;

    protected override void OnEnable()
    {
        base.OnEnable();
        ResolutionSettings.OnResolutionSelected += ApplyResolution;
        QualitySettings.OnQualityLevelSelected += ApplyQualityLevel;
        FullScreenSettings.OnFullScreenChanged += ApplyFullScreen;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ResolutionSettings.OnResolutionSelected -= ApplyResolution;
        QualitySettings.OnQualityLevelSelected -= ApplyQualityLevel;
        FullScreenSettings.OnFullScreenChanged -= ApplyFullScreen;
    }

    private void ApplyResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height,
            Screen.fullScreenMode, resolution.refreshRateRatio);
    }

    private void ApplyQualityLevel(int level)
    {
        UnityEngine.QualitySettings.SetQualityLevel(level, true);
    }

    private void ApplyFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
