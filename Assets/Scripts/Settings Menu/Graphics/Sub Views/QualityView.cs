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

    private GraphicsSettingsEntity graphicsSettings;
    private LocalizedString qualityLevelName;

    public void Dependencies(GraphicsSettingsEntity graphicsSettings)
    {
        this.graphicsSettings = graphicsSettings;
    }

    public void Initialize()
    {
        previousQualityButton.onClick.AddListener(OnPreviousQualityLevel);
        nextQualityButton.onClick.AddListener(OnNextQualityLevel);

        qualityListController.OnPreviousItemRequested += OnPreviousQualityLevel;
        qualityListController.OnNextItemRequested += OnNextQualityLevel;

        graphicsSettings.OnQualityLevelChanged += UpdateQualityLevelButtons;
        graphicsSettings.OnQualityLevelChanged += UpdateQualityLevelText;

        InitializeSelectors();
    }

    public void Conclude()
    {
        previousQualityButton.onClick.RemoveListener(OnPreviousQualityLevel);
        nextQualityButton.onClick.RemoveListener(OnNextQualityLevel);

        qualityListController.OnPreviousItemRequested -= OnPreviousQualityLevel;
        qualityListController.OnNextItemRequested -= OnNextQualityLevel;

        graphicsSettings.OnQualityLevelChanged -= UpdateQualityLevelButtons;
        graphicsSettings.OnQualityLevelChanged -= UpdateQualityLevelText;

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
        int currentIndex = graphicsSettings.CurrentQualityLevelIndex - 1;
        graphicsSettings.SetQualityLevelIndex(currentIndex);
    }

    private void OnNextQualityLevel()
    {
        int currentIndex = graphicsSettings.CurrentQualityLevelIndex + 1;
        graphicsSettings.SetQualityLevelIndex(currentIndex);
    }

    private void UpdateQualityLevelButtons()
    {
        int currentIndex = graphicsSettings.CurrentQualityLevelIndex;
        previousQualityButton.gameObject.SetActive(currentIndex > 0);
        nextQualityButton.gameObject.SetActive(currentIndex < graphicsSettings.AvailableQualityLevels.Count - 1);
    }

    private void UpdateQualityLevelText()
    {
        if (qualityLevelName != null)
            qualityLevelName.StringChanged -= OnQualityLevelChanged;

        string currentQualityLevel = graphicsSettings.AvailableQualityLevels[graphicsSettings.CurrentQualityLevelIndex];

        qualityLevelName = new LocalizedString
        {
            TableReference = SETTINGS_TABLE_REFERENCE,
            TableEntryReference = $"{QUALITIES_ENTRY_REFERENCE}{currentQualityLevel}"
        };

        qualityLevelName.StringChanged += OnQualityLevelChanged;
        qualityLevelName.RefreshString();
    }

    private void OnQualityLevelChanged(string value) => qualityLevelText.text = value;
}
