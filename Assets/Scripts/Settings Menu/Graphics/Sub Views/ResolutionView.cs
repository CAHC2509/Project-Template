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

    private IGraphicsSettingsController controller;

    public void Initialize(IGraphicsSettingsController controller)
    {
        this.controller = controller;

        previousResolutionButton.onClick.AddListener(OnPreviousResolution);
        nextResolutionButton.onClick.AddListener(OnNextResolution);

        resolutionListController.OnPreviousItemRequested += OnPreviousResolution;
        resolutionListController.OnNextItemRequested += OnNextResolution;

        InitializeSelectors();
    }

    public void Conclude()
    {
        previousResolutionButton.onClick.RemoveListener(OnPreviousResolution);
        nextResolutionButton.onClick.RemoveListener(OnNextResolution);

        resolutionListController.OnPreviousItemRequested -= OnPreviousResolution;
        resolutionListController.OnNextItemRequested -= OnNextResolution;
    }

    private void InitializeSelectors()
    {
        UpdateResolutionButtons();
        UpdateResolutionText();
    }

    private void OnPreviousResolution()
    {
        int index = controller.GetModel().CurrentResolutionIndex - 1;
        controller.SetResolution(index);
    }

    private void OnNextResolution()
    {
        int index = controller.GetModel().CurrentResolutionIndex + 1;
        controller.SetResolution(index);
    }

    public void UpdateResolutionButtons()
    {
        int currentIndex = controller.GetModel().CurrentResolutionIndex;
        previousResolutionButton.gameObject.SetActive(currentIndex > 0);
        nextResolutionButton.gameObject.SetActive(currentIndex < controller.GetModel().AvailableResolutions.Count - 1);
    }

    public void UpdateResolutionText()
    {
        Resolution currentResolution = controller.GetModel().AvailableResolutions[controller.GetModel().CurrentResolutionIndex];
        resolutionText.text = $"{currentResolution.width}x{currentResolution.height}";
    }
}