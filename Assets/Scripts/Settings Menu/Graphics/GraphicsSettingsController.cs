using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettingsController : ControllerBase, ISettings
{
    [SerializeField] private GraphicsSettingsView view;

    private const string RESOLUTIONS_KEY = "Resolution";
    private const string QUALITY_LEVELS_KEY = "QualityLevel";
    private const string FULL_SCREEN_KEY = "FullScreen";

    private GraphicsSettingsEntity graphicsSettings;

    public void Dependencies()
    {
        LoadAvailiableSettings();
        view.Dependencies(graphicsSettings);
    }

    public override void Initialize()
    {
        base.Initialize();

        LoadSettings();
        view.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
    }

    public void LoadSettings()
    {
        graphicsSettings.SetResolutionIndex(PlayerPrefs.GetInt(RESOLUTIONS_KEY, graphicsSettings.AvailableResolutions.Count - 1));
        graphicsSettings.SetQualityLevelIndex(PlayerPrefs.GetInt(QUALITY_LEVELS_KEY, graphicsSettings.AvailableQualityLevels.Count - 1));
        graphicsSettings.SetFullscreenMode(PlayerPrefs.GetInt(FULL_SCREEN_KEY, 1) == 1 ? true : false);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(RESOLUTIONS_KEY, graphicsSettings.CurrentResolutionIndex);
        PlayerPrefs.SetInt(QUALITY_LEVELS_KEY, graphicsSettings.CurrentQualityLevelIndex);
        PlayerPrefs.SetInt(FULL_SCREEN_KEY, graphicsSettings.CurrentFullScreenMode ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadAvailiableSettings()
    {
        List<Resolution> resolutions = GetAvailiableResolutions();
        List<string> qualityLevels = GetAvailiableQualityLevels();
        graphicsSettings = new GraphicsSettingsEntity(resolutions, qualityLevels);
    }

    protected override void AddListeners()
    {
        graphicsSettings.OnResolutionChanged += SetResolution;
        graphicsSettings.OnQualityLevelChanged += SetQualityLevel;
        graphicsSettings.OnFullScreenChanged += SetFullScreenMode;
    }

    protected override void RemoveListeners()
    {
        graphicsSettings.OnResolutionChanged -= SetResolution;
        graphicsSettings.OnQualityLevelChanged -= SetQualityLevel;
        graphicsSettings.OnFullScreenChanged -= SetFullScreenMode;
    }

    private void SetResolution()
    {
        Resolution selectedResolution = graphicsSettings.AvailableResolutions[graphicsSettings.CurrentResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, graphicsSettings.CurrentFullScreenMode);
        SaveSettings();
    }

    private void SetQualityLevel()
    {
        int selectedQualityLevel = graphicsSettings.CurrentQualityLevelIndex;
        QualitySettings.SetQualityLevel(selectedQualityLevel);
        SaveSettings();
    }

    private void SetFullScreenMode()
    {
        bool selectedFullScreenMode = graphicsSettings.CurrentFullScreenMode;
        Screen.fullScreenMode = selectedFullScreenMode ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        SaveSettings();
    }

    private List<Resolution> GetAvailiableResolutions()
    {
        List<Resolution> filteredResolutions = new List<Resolution>();
        foreach (Resolution resolution in Screen.resolutions)
        {
            float aspectRatio = (float)resolution.width / resolution.height;
            if (aspectRatio >= 1.77f && aspectRatio <= 1.78f)
                filteredResolutions.Add(resolution);
        }
        return filteredResolutions.Count > 0 ? filteredResolutions : new List<Resolution>(Screen.resolutions);
    }

    private List<string> GetAvailiableQualityLevels() => new List<string>(QualitySettings.names);
}
