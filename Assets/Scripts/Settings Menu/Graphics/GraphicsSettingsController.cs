using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettingsController : ControllerBase, IGraphicsSettingsController
{
    private ISaveSystem saveSystem;
    private IGraphicSettingsView view;
    private GraphicsSettingsEntity graphicsSettings;
    private GraphicsSettingsEntity previousSettings;

    private void Awake()
    {
        view = GetComponentInChildren<IGraphicSettingsView>();
    }

    public void Dependencies(ISaveSystem saveSystem)
    {
        this.saveSystem = saveSystem;
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
        if (saveSystem.HasKey(Constants.Settings.GRAPHICS_SETTINGS_KEY))
        {
            List<Resolution> resolutions = GetAvailiableResolutions();
            List<string> qualityLevels = GetAvailiableQualityLevels();

            graphicsSettings = (GraphicsSettingsEntity)saveSystem.Load(Constants.Settings.GRAPHICS_SETTINGS_KEY, typeof(GraphicsSettingsEntity));
            graphicsSettings.SetAvailiableSettings(resolutions, qualityLevels);
        }
        else
        {
            LoadAvailiableSettings();
            graphicsSettings.SetResolutionIndex(graphicsSettings.AvailableResolutions.Count - 1);
            graphicsSettings.SetQualityLevelIndex(graphicsSettings.AvailableQualityLevels.Count - 1);
            graphicsSettings.SetFullscreenMode(true);
        }

        ApplyCurrentSettings();
    }

    public void SaveSettings()
    {
        saveSystem.Save(Constants.Settings.GRAPHICS_SETTINGS_KEY, graphicsSettings);
        graphicsSettings.CleanPendingChanges();
    }

    public void SetResolution(int index)
    {
        index = Mathf.Clamp(index, 0, graphicsSettings.AvailableResolutions.Count - 1);
        graphicsSettings.SetResolutionIndex(index);
        view.UpdateResolutionView();

        Resolution selectedResolution = graphicsSettings.AvailableResolutions[graphicsSettings.CurrentResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, graphicsSettings.CurrentFullScreenMode);
    }

    public void SetQualityLevel(int index)
    {
        index = Mathf.Clamp(index, 0, graphicsSettings.AvailableQualityLevels.Count - 1);
        graphicsSettings.SetQualityLevelIndex(index);
        view.UpdateQualityLevelView();
        QualitySettings.SetQualityLevel(graphicsSettings.CurrentQualityLevelIndex);
    }

    public void SetFullScreenMode(bool activeMode)
    {
        graphicsSettings.SetFullscreenMode(activeMode);
        view.UpdateFullScreenModeView();
        Screen.fullScreenMode = graphicsSettings.CurrentFullScreenMode ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
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

    private void LoadAvailiableSettings()
    {
        List<Resolution> resolutions = GetAvailiableResolutions();
        List<string> qualityLevels = GetAvailiableQualityLevels();
        graphicsSettings = new GraphicsSettingsEntity();
        graphicsSettings.SetAvailiableSettings(resolutions, qualityLevels);
    }

    private void ApplyCurrentSettings()
    {
        Resolution selectedResolution = graphicsSettings.AvailableResolutions[graphicsSettings.CurrentResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, graphicsSettings.CurrentFullScreenMode);
        QualitySettings.SetQualityLevel(graphicsSettings.CurrentQualityLevelIndex);
        Screen.fullScreenMode = graphicsSettings.CurrentFullScreenMode ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void SetDefaultSettings()
    {
        graphicsSettings.SetResolutionIndex(graphicsSettings.AvailableResolutions.Count - 1);
        graphicsSettings.SetQualityLevelIndex(graphicsSettings.AvailableQualityLevels.Count - 1);
        graphicsSettings.SetFullscreenMode(true);
        ApplyCurrentSettings();
        UpdateView();
    }

    private List<string> GetAvailiableQualityLevels()
    {
        return new List<string>(QualitySettings.names);
    }

    public GraphicsSettingsEntity GetModel()
    {
        return graphicsSettings;
    }

    private void UpdateView()
    {
        view.UpdateResolutionView();
        view.UpdateQualityLevelView();
        view.UpdateFullScreenModeView();
    }

    public void EnableView()
    {
        view.EnableView();
        graphicsSettings.CleanPendingChanges();
        previousSettings = new GraphicsSettingsEntity();
        previousSettings.SetAvailiableSettings(graphicsSettings.AvailableResolutions, graphicsSettings.AvailableQualityLevels);
        previousSettings.SetResolutionIndex(graphicsSettings.CurrentResolutionIndex);
        previousSettings.SetQualityLevelIndex(graphicsSettings.CurrentQualityLevelIndex);
        previousSettings.SetFullscreenMode(graphicsSettings.CurrentFullScreenMode);
    }

    public void DisableView()
    {
        view.DisableView();

        if (graphicsSettings.PendingChanges)
            graphicsSettings = previousSettings;
    }
}
