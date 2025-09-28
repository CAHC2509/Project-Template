using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GraphicsSettingsEntity
{
    [SerializeField] private int currentResolutionIndex;
    [SerializeField] private int currentQualityLevelIndex;
    [SerializeField] private bool currentFullScreenMode;

    private List<Resolution> availableResolutions;
    private List<string> availableQualityLevels;
    public List<Resolution> AvailableResolutions => availableResolutions;
    public List<string> AvailableQualityLevels => availableQualityLevels;
    public int CurrentResolutionIndex => currentResolutionIndex;
    public int CurrentQualityLevelIndex => currentQualityLevelIndex;
    public bool CurrentFullScreenMode => currentFullScreenMode;

    public void SetAvailiableSettings(List<Resolution> resolutions, List<string> qualityLevels)
    {
        availableResolutions = resolutions;
        availableQualityLevels = qualityLevels;
    }

    public void SetResolutionIndex(int resolutionIndex)
    {
        currentResolutionIndex = resolutionIndex;
    }

    public void SetQualityLevelIndex(int qualityLevelIndex)
    {
        currentQualityLevelIndex = qualityLevelIndex;
    }

    public void SetFullscreenMode(bool activeMode)
    {
        currentFullScreenMode = activeMode;
    }
}
