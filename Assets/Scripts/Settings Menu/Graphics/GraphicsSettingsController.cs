using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettingsController : ControllerBase, IGraphicsSettingsController
{
    [SerializeField] private SaveGraphicsSettingsModalWindowView modalWindow;

    private ISaveSystem saveSystem;
    private IGraphicSettingsView view;
    private GraphicsSettingsEntity graphicsSettings;

    private void Awake()
    {
        view = GetComponentInChildren<IGraphicSettingsView>();
    }

    public void Dependencies(ISaveSystem saveSystem)
    {
        this.saveSystem = saveSystem;
        modalWindow.Dependencies(this);
    }

    public override void Initialize()
    {
        base.Initialize();

        LoadSettings();
        view.Initialize();
        modalWindow.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
        modalWindow.Conclude();
    }

    public void LoadSettings()
    {
        List<Resolution> resolutions = GetAvailiableResolutions();
        List<string> qualityLevels = GetAvailiableQualityLevels();

        if (saveSystem.HasKey(Constants.GRAPHICS_SETTINGS_KEY))
        {
            graphicsSettings = (GraphicsSettingsEntity)saveSystem.Load(Constants.GRAPHICS_SETTINGS_KEY, typeof(GraphicsSettingsEntity));
            graphicsSettings.SetAvailiableSettings(resolutions, qualityLevels);
        }
        else
        {
            graphicsSettings = new GraphicsSettingsEntity();
            graphicsSettings.SetAvailiableSettings(resolutions, qualityLevels);
            graphicsSettings.SetResolutionIndex(graphicsSettings.AvailableResolutions.Count - 1);
            graphicsSettings.SetQualityLevelIndex(graphicsSettings.AvailableQualityLevels.Count - 1);
            graphicsSettings.SetFullscreenMode(true);
        }
    }

    public void SaveSettings()
    {
        saveSystem.Save(Constants.GRAPHICS_SETTINGS_KEY, graphicsSettings);
        graphicsSettings.CleanPendingChanges();
    }

    public void SetResolution(int index)
    {
        index = Mathf.Clamp(index, 0, graphicsSettings.AvailableResolutions.Count - 1);
        graphicsSettings.SetResolutionIndex(index);

        Resolution selectedResolution = graphicsSettings.AvailableResolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, graphicsSettings.CurrentFullScreenMode);

        view.UpdateResolutionView();
    }

    public void SetQualityLevel(int index)
    {
        index = Mathf.Clamp(index, 0, graphicsSettings.AvailableQualityLevels.Count - 1);
        graphicsSettings.SetQualityLevelIndex(index);
        QualitySettings.SetQualityLevel(index);

        view.UpdateQualityLevelView();
    }

    public void SetFullScreenMode(bool activeMode)
    {
        graphicsSettings.SetFullscreenMode(activeMode);
        Screen.fullScreenMode = activeMode ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

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

    private List<string> GetAvailiableQualityLevels()
    {
        return new List<string>(QualitySettings.names);
    }

    public GraphicsSettingsEntity GetModel()
    {
        return graphicsSettings;
    }

    public void EnableView()
    {
        view.EnableView();
        graphicsSettings.CleanPendingChanges();
    }

    public void DisableView()
    {
        if (!graphicsSettings.PendingChanges)
            view.DisableView();
        else
            modalWindow.EnableView();
    }
}
