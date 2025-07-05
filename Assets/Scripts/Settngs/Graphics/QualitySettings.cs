using UnityEngine;
using System.Collections.Generic;
using System;

public class QualitySettings : SelectableListItem
{
    [Space, Header("Quality components")]
    [SerializeField] private GraphicsData graphicsData;

    private List<string> availableQualityLevels;
    private int currentIndex = 0;

    public static event Action<int> OnQualityLevelSelected;

    private void Start()
    {
        availableQualityLevels = graphicsData.QualityLevels;
        currentIndex = GraphicsQualitySettings.GetQualityLevel(graphicsData);
        UpdateQualityText(availableQualityLevels[currentIndex]);
        UpdateButtonStates();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GraphicsQualityApplier.OnQualityLevelApplied += SetQualityValue;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GraphicsQualityApplier.OnQualityLevelApplied -= SetQualityValue;
    }

    protected override void NextItem()
    {
        if (currentIndex < availableQualityLevels.Count - 1)
        {
            OnListChanged?.Invoke();
            currentIndex++;
            ApplyQuality(currentIndex);
        }
    }

    protected override void PreviousItem()
    {
        if (currentIndex > 0)
        {
            OnListChanged?.Invoke();
            currentIndex--;
            ApplyQuality(currentIndex);
        }
    }

    private void SetQualityValue(int index)
    {
        currentIndex = Mathf.Clamp(index, 0, availableQualityLevels.Count - 1);
        UpdateQualityText(availableQualityLevels[currentIndex]);
        UpdateButtonStates();
    }

    private void ApplyQuality(int index)
    {
        OnQualityLevelSelected?.Invoke(index);
        GraphicsQualitySettings.SetQualityLevel(graphicsData, index);
        UpdateQualityText(availableQualityLevels[index]);
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        previousButton.gameObject.SetActive(currentIndex > 0);
        nextButton.gameObject.SetActive(currentIndex < availableQualityLevels.Count - 1);
        AutoSelect();
    }

    private void UpdateQualityText(string qualityName) => itemText.text = qualityName;
}
