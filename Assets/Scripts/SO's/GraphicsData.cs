using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings/Graphics Data", fileName = "Graphics Data")]
public class GraphicsData : ScriptableObject
{
    [field: SerializeField] public string ResolutionWidthKey { get; private set; }
    [field: SerializeField] public string ResolutionHeightKey { get; private set; }
    [field: SerializeField] public string QualityKey { get; private set; }
    [field: SerializeField] public string FullScreenKey { get; private set; }
    [field: SerializeField] public List<Resolution> AvailableResolutions { get; private set; }
    [field: SerializeField] public List<string> QualityLevels { get; private set; }
    [field: SerializeField] public bool FullScreenActive { get; private set; }

    public void SetResolutions(List<Resolution> resolutions) => AvailableResolutions = resolutions;
    public void SetQualityLevels(List<string> qualityLevels) => QualityLevels = qualityLevels;
    public void SetFullScreenValue(bool value) => FullScreenActive = value;
}
