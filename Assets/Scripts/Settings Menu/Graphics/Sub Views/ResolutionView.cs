using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ResolutionView
{
    [SerializeField] private ListStateController resolutionListController;
    [SerializeField] private Button previousResolutionButton;
    [SerializeField] private Button nextResolutionButton;
    [SerializeField] private TextMeshProUGUI resolutionText;

    private GraphicsSettingsEntity graphicsSettings;

    public void Dependencies(GraphicsSettingsEntity graphicsSettings)
    {
        this.graphicsSettings = graphicsSettings;
    }

    public void Initialize()
    {
        previousResolutionButton.onClick.AddListener(OnPreviousResolution);
        nextResolutionButton.onClick.AddListener(OnNextResolution);

        resolutionListController.OnPreviousItemRequested += OnPreviousResolution;
        resolutionListController.OnNextItemRequested += OnNextResolution;

        graphicsSettings.OnResolutionChanged += UpdateResolutionButtons;
        graphicsSettings.OnResolutionChanged += UpdateResolutionText;

        InitializeSelectors();
    }

    public void Conclude()
    {
        previousResolutionButton.onClick.RemoveListener(OnPreviousResolution);
        nextResolutionButton.onClick.RemoveListener(OnNextResolution);

        resolutionListController.OnPreviousItemRequested -= OnPreviousResolution;
        resolutionListController.OnNextItemRequested -= OnNextResolution;

        graphicsSettings.OnResolutionChanged -= UpdateResolutionButtons;
        graphicsSettings.OnResolutionChanged -= UpdateResolutionText;
    }

    private void InitializeSelectors()
    {
        UpdateResolutionButtons();
        UpdateResolutionText();
    }

    private void OnPreviousResolution()
    {
        int currentIndex = graphicsSettings.CurrentResolutionIndex - 1;
        graphicsSettings.SetResolutionIndex(currentIndex);
    }

    private void OnNextResolution()
    {
        int currentIndex = graphicsSettings.CurrentResolutionIndex + 1;
        graphicsSettings.SetResolutionIndex(currentIndex);
    }

    private void UpdateResolutionButtons()
    {
        int currentIndex = graphicsSettings.CurrentResolutionIndex;
        previousResolutionButton.gameObject.SetActive(currentIndex > 0);
        nextResolutionButton.gameObject.SetActive(currentIndex < graphicsSettings.AvailableResolutions.Count - 1);
    }

    private void UpdateResolutionText()
    {
        Resolution currentResolution = graphicsSettings.AvailableResolutions[graphicsSettings.CurrentResolutionIndex];
        resolutionText.text = $"{currentResolution.width}x{currentResolution.height}";
    }
}