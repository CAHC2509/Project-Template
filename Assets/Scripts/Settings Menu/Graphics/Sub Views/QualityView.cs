using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using TMPro;

[Serializable]
public class QualityView
{
    [SerializeField] private ListStateController qualityListController;
    [SerializeField] private Button previousQualityButton;
    [SerializeField] private Button nextQualityButton;
    [SerializeField] private TextMeshProUGUI qualityLevelText;

    private const string SETTINGS_TABLE_REFERENCE = "Settings Menu";
    private const string QUALITIES_ENTRY_REFERENCE = "settings.qualities.";

    private IGraphicsSettingsController controller;
    private LocalizedString qualityLevelName;

    public void Initialize(IGraphicsSettingsController controller)
    {
        this.controller = controller;

        previousQualityButton.onClick.AddListener(OnPreviousQualityLevel);
        nextQualityButton.onClick.AddListener(OnNextQualityLevel);

        qualityListController.OnPreviousItemRequested += OnPreviousQualityLevel;
        qualityListController.OnNextItemRequested += OnNextQualityLevel;

        InitializeSelectors();
    }

    public void Conclude()
    {
        previousQualityButton.onClick.RemoveListener(OnPreviousQualityLevel);
        nextQualityButton.onClick.RemoveListener(OnNextQualityLevel);

        qualityListController.OnPreviousItemRequested -= OnPreviousQualityLevel;
        qualityListController.OnNextItemRequested -= OnNextQualityLevel;

        if (qualityLevelName != null)
            qualityLevelName.StringChanged -= OnQualityLevelChanged;
    }

    private void InitializeSelectors()
    {
        UpdateQualityLevelButtons();
        UpdateQualityLevelText();
    }

    private void OnPreviousQualityLevel()
    {
        int index = controller.GetModel().CurrentQualityLevelIndex - 1;
        controller.SetQualityLevel(index);
    }

    private void OnNextQualityLevel()
    {
        int index = controller.GetModel().CurrentQualityLevelIndex + 1;
        controller.SetQualityLevel(index);
    }

    public void UpdateQualityLevelButtons()
    {
        int index = controller.GetModel().CurrentQualityLevelIndex;
        previousQualityButton.gameObject.SetActive(index > 0);
        nextQualityButton.gameObject.SetActive(index < controller.GetModel().AvailableQualityLevels.Count - 1);
    }

    public void UpdateQualityLevelText()
    {
        if (qualityLevelName != null)
            qualityLevelName.StringChanged -= OnQualityLevelChanged;

        string currentQualityLevel = controller.GetModel().AvailableQualityLevels[controller.GetModel().CurrentQualityLevelIndex];

        qualityLevelName = new LocalizedString
        {
            TableReference = SETTINGS_TABLE_REFERENCE,
            TableEntryReference = $"{QUALITIES_ENTRY_REFERENCE}{currentQualityLevel}"
        };

        qualityLevelName.StringChanged += OnQualityLevelChanged;
        qualityLevelName.RefreshString();
    }

    private void OnQualityLevelChanged(string value)
    {
        qualityLevelText.text = value;
    }
}
