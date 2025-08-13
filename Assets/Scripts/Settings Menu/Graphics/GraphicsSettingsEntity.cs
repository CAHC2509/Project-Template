using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GraphicsSettingsEntity
{
    public List<Resolution> AvailableResolutions { get; private set; }
    public List<string> AvailableQualityLevels { get; private set; }
    public int CurrentResolutionIndex { get; private set; }
    public int CurrentQualityLevelIndex { get; private set; }
    public bool CurrentFullScreenMode { get; private set; }

    public GraphicsSettingsEntity(List<Resolution> resolutions, List<string> qualityLevels)
    {
        AvailableResolutions = resolutions;
        AvailableQualityLevels = qualityLevels;
    }

    public void SetResolutionIndex(int resolutionIndex)
    {
        CurrentResolutionIndex = resolutionIndex;
    }

    public void SetQualityLevelIndex(int qualityLevelIndex)
    {
        CurrentQualityLevelIndex = qualityLevelIndex;
    }

    public void SetFullscreenMode(bool activeMode)
    {
        CurrentFullScreenMode = activeMode;
    }
}
