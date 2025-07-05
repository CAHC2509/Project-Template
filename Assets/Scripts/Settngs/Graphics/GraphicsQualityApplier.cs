using System;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsQualityApplier
{
    private readonly GraphicsData graphicsData;

    public GraphicsQualityApplier(GraphicsData graphicsData)
    {
        this.graphicsData = graphicsData;
        LoadQualityLevels();
    }

    public static event Action<int> OnQualityLevelApplied;

    private void LoadQualityLevels()
    {
        List<string> availableQualityLevels = new List<string>(UnityEngine.QualitySettings.names);
        graphicsData.SetQualityLevels(availableQualityLevels);
    }

    public void ApplyQualityLevel(int index)
    {
        UnityEngine.QualitySettings.SetQualityLevel(index);
        OnQualityLevelApplied?.Invoke(index);
    }

    public void ApplySavedQualityLevel()
    {
        int savedIndex = GraphicsQualitySettings.GetQualityLevel(graphicsData);
        ApplyQualityLevel(savedIndex);
    }
}
