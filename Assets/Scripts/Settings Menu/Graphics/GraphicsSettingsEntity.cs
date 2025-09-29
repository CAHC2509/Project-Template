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
    private bool pendingChanges;

    public List<Resolution> AvailableResolutions => availableResolutions;
    public List<string> AvailableQualityLevels => availableQualityLevels;
    public int CurrentResolutionIndex => currentResolutionIndex;
    public int CurrentQualityLevelIndex => currentQualityLevelIndex;
    public bool CurrentFullScreenMode => currentFullScreenMode;
    public bool PendingChanges => pendingChanges;

    public void SetAvailiableSettings(List<Resolution> resolutions, List<string> qualityLevels)
    {
        availableResolutions = resolutions;
        availableQualityLevels = qualityLevels;
        pendingChanges = false;
    }

    public void SetResolutionIndex(int resolutionIndex)
    {
        currentResolutionIndex = resolutionIndex;
        pendingChanges = true;
    }

    public void SetQualityLevelIndex(int qualityLevelIndex)
    {
        currentQualityLevelIndex = qualityLevelIndex;
        pendingChanges = true;
    }

    public void SetFullscreenMode(bool activeMode)
    {
        currentFullScreenMode = activeMode;
        pendingChanges = true;
    }

    public void CleanPendingChanges()
    {
        pendingChanges = false;
    }
}
