using System;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsResolutionApplier
{
    private readonly GraphicsData graphicsData;

    public static event Action<Resolution> OnResolutionApplied;

    public GraphicsResolutionApplier(GraphicsData graphicsData)
    {
        this.graphicsData = graphicsData;
        LoadResolutions();
    }

    private void LoadResolutions()
    {
        List<Resolution> resolutions = GetAvailableResolutions();
        graphicsData.SetResolutions(resolutions);
    }

    public void ApplyResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        GraphicsResolutionSettings.SaveResolution(graphicsData, resolution);
        OnResolutionApplied?.Invoke(resolution);
    }

    public void ApplySavedResolution()
    {
        Resolution savedResolution = GraphicsResolutionSettings.GetSavedResolution(graphicsData);
        ApplyResolution(savedResolution);
    }

    public List<Resolution> GetAvailableResolutions()
    {
        List<Resolution> filtered = new List<Resolution>();
        HashSet<string> seen = new HashSet<string>();

        foreach (Resolution res in Screen.resolutions)
        {
            float aspect = (float)res.width / res.height;
            if (Mathf.Abs(aspect - 16f / 9f) > 0.05f) continue;

            string key = $"{res.width}x{res.height}";
            if (seen.Contains(key)) continue;

            seen.Add(key);
            filtered.Add(res);
        }

        return filtered;
    }
}
