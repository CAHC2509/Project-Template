using System;
using UnityEngine;

public class ResolutionSettings : SelectableListItem
{
    [Space, Header("Resolution components")]
    [SerializeField] private GraphicsData graphicsData;

    private int currentIndex = 0;

    public static event Action<Resolution> OnResolutionSelected;

    protected override void OnEnable()
    {
        base.OnEnable();
        GraphicsResolutionApplier.OnResolutionApplied += SetResolutionValue;

        Resolution currentResolution = GraphicsResolutionSettings.GetSavedResolution(graphicsData);
        currentIndex = GetClosestResolutionIndex(currentResolution);
        UpdateResolutionText(graphicsData.AvailableResolutions[currentIndex]);
        UpdateButtonStates();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GraphicsResolutionApplier.OnResolutionApplied -= SetResolutionValue;
    }

    protected override void NextItem()
    {
        if (currentIndex < graphicsData.AvailableResolutions.Count - 1)
        {
            OnListChanged?.Invoke();
            currentIndex++;
            ApplyResolution(currentIndex);
        }
    }

    protected override void PreviousItem()
    {
        if (currentIndex > 0)
        {
            OnListChanged?.Invoke();
            currentIndex--;
            ApplyResolution(currentIndex);
        }
    }

    private void SetResolutionValue(Resolution resolution)
    {
        currentIndex = GetClosestResolutionIndex(resolution);
        UpdateResolutionText(resolution);
        UpdateButtonStates();
    }

    private void ApplyResolution(int index)
    {
        Resolution resolution = graphicsData.AvailableResolutions[index];
        OnResolutionSelected?.Invoke(resolution);
        GraphicsResolutionSettings.SaveResolution(graphicsData, resolution);
        UpdateResolutionText(resolution);
        UpdateButtonStates();
        AutoSelect();
    }

    private void UpdateButtonStates()
    {
        previousButton.gameObject.SetActive(currentIndex > 0);
        nextButton.gameObject.SetActive(currentIndex < graphicsData.AvailableResolutions.Count - 1);
        AutoSelect();
    }

    private void UpdateResolutionText(Resolution resolution) => itemText.text = $"{resolution.width}x{resolution.height}";

    private int GetClosestResolutionIndex(Resolution target)
    {
        for (int i = 0; i < graphicsData.AvailableResolutions.Count; i++)
        {
            Resolution resolution = graphicsData.AvailableResolutions[i];
            if (resolution.width == target.width && resolution.height == target.height)
                return i;
        }
        return 0;
    }
}
