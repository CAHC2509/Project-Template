using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettingsController : ControllerBase, IGraphicsSettingsController
{
    private IGraphicSettingsView view;
    private GraphicsSettingsEntity graphicsSettings;

    private void Awake()
    {
        view = GetComponentInChildren<IGraphicSettingsView>();
        LoadAvailiableSettings();
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

    private void LoadAvailiableSettings()
    {
        List<Resolution> resolutions = GetAvailiableResolutions();
        List<string> qualityLevels = GetAvailiableQualityLevels();
        graphicsSettings = new GraphicsSettingsEntity(resolutions, qualityLevels);
    }

    public void LoadSettings()
    {
        graphicsSettings.SetResolutionIndex(PlayerPrefs.GetInt(Constants.RESOLUTIONS_KEY, graphicsSettings.AvailableResolutions.Count - 1));
        graphicsSettings.SetQualityLevelIndex(PlayerPrefs.GetInt(Constants.QUALITY_LEVELS_KEY, graphicsSettings.AvailableQualityLevels.Count - 1));
        graphicsSettings.SetFullscreenMode(PlayerPrefs.GetInt(Constants.FULL_SCREEN_KEY, 1) == 1 ? true : false);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(Constants.RESOLUTIONS_KEY, graphicsSettings.CurrentResolutionIndex);
        PlayerPrefs.SetInt(Constants.QUALITY_LEVELS_KEY, graphicsSettings.CurrentQualityLevelIndex);
        PlayerPrefs.SetInt(Constants.FULL_SCREEN_KEY, graphicsSettings.CurrentFullScreenMode ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetResolution(int index)
    {
        index = Mathf.Clamp(index, 0, graphicsSettings.AvailableResolutions.Count - 1);
        graphicsSettings.SetResolutionIndex(index);

        Resolution selectedResolution = graphicsSettings.AvailableResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, graphicsSettings.CurrentFullScreenMode);
        SaveSettings();

        view.UpdateResolutionView();
    }

    public void SetQualityLevel(int index)
    {
        index = Mathf.Clamp(index, 0, graphicsSettings.AvailableQualityLevels.Count - 1);
        graphicsSettings.SetQualityLevelIndex(index);

        QualitySettings.SetQualityLevel(index);
        SaveSettings();

        view.UpdateQualityLevelView();
    }

    public void SetFullScreenMode(bool activeMode)
    {
        graphicsSettings.SetFullscreenMode(activeMode);

        Screen.fullScreenMode = activeMode ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        SaveSettings();

        view.UpdateFullScreenModeView();
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

    public GraphicsSettingsEntity GetModel()
    {
        return graphicsSettings;
    }

    public void EnableView() => view.EnableView();
    public void DisableView() => view.DisableView();
}
