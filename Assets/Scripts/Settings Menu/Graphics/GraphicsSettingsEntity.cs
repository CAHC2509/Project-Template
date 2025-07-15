using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraphicsSettingsEntity
{
    public List<Resolution> AvailableResolutions { get; private set; }
    public List<string> AvailableQualityLevels { get; private set; }
    public int CurrentResolutionIndex { get; private set; }
    public int CurrentQualityLevelIndex { get; private set; }
    public bool CurrentFullScreenMode { get; private set; }

    public event Action OnResolutionChanged;
    public event Action OnQualityLevelChanged;
    public event Action OnFullScreenChanged;

    public GraphicsSettingsEntity(List<Resolution> resolutions, List<string> qualityLevels)
    {
        AvailableResolutions = resolutions;
        AvailableQualityLevels = qualityLevels;
    }

    public void SetResolutionIndex(int resolutionIndex)
    {
        resolutionIndex = Mathf.Clamp(resolutionIndex, 0, AvailableResolutions.Count - 1);
        CurrentResolutionIndex = resolutionIndex;
        OnResolutionChanged?.Invoke();
    }

    public void SetQualityLevelIndex(int qualityLevelIndex)
    {
        qualityLevelIndex = Mathf.Clamp(qualityLevelIndex, 0, AvailableQualityLevels.Count - 1);
        CurrentQualityLevelIndex = qualityLevelIndex;
        OnQualityLevelChanged?.Invoke();
    }

    public void SetFullscreenMode(bool activeMode)
    {
        CurrentFullScreenMode = activeMode;
        OnFullScreenChanged?.Invoke();
    }
}
