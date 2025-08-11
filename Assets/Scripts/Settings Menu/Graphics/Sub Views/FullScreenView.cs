using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using TMPro;

[Serializable]
public class FullScreenView
{
    [SerializeField] private ListStateController fullScreenListController;
    [SerializeField] private Button previousFullScreenButton;
    [SerializeField] private Button nextFullScreenButton;
    [SerializeField] private TextMeshProUGUI fullScreenModeText;

    private const string SETTINGS_TABLE_REFERENCE = "Settings Menu";
    private const string FULLSCREEN_MODES_ENTRY_REFERENCE = "settings.fullScreen.";

    private GraphicsSettingsEntity graphicsSettings;
    private LocalizedString fullScreenModeName;

    public void Dependencies(GraphicsSettingsEntity graphicsSettings)
    {
        this.graphicsSettings = graphicsSettings;
    }

    public void Initialize()
    {
        previousFullScreenButton.onClick.AddListener(OnDisableFullScreenMode);
        nextFullScreenButton.onClick.AddListener(OnEnableFullScreenMode);

        fullScreenListController.OnPreviousItemRequested += OnDisableFullScreenMode;
        fullScreenListController.OnNextItemRequested += OnEnableFullScreenMode;

        graphicsSettings.OnFullScreenChanged += UpdateFullScreenModeButtons;
        graphicsSettings.OnFullScreenChanged += UpdateFullScreenModeText;

        InitializeSelectors();
    }

    public void Conclude()
    {
        previousFullScreenButton.onClick.RemoveListener(OnDisableFullScreenMode);
        nextFullScreenButton.onClick.RemoveListener(OnEnableFullScreenMode);

        fullScreenListController.OnPreviousItemRequested -= OnDisableFullScreenMode;
        fullScreenListController.OnNextItemRequested -= OnEnableFullScreenMode;

        graphicsSettings.OnFullScreenChanged -= UpdateFullScreenModeButtons;
        graphicsSettings.OnFullScreenChanged -= UpdateFullScreenModeText;

        if (fullScreenModeName != null)
            fullScreenModeName.StringChanged -= OnFullScreenModeChanged;
    }

    private void InitializeSelectors()
    {
        UpdateFullScreenModeButtons();
        UpdateFullScreenModeText();
    }

    private void OnDisableFullScreenMode() => graphicsSettings.SetFullscreenMode(false);
    private void OnEnableFullScreenMode() => graphicsSettings.SetFullscreenMode(true);

    private void UpdateFullScreenModeButtons()
    {
        bool activeMode = graphicsSettings.CurrentFullScreenMode;
        previousFullScreenButton.gameObject.SetActive(activeMode);
        nextFullScreenButton.gameObject.SetActive(!activeMode);
    }

    private void UpdateFullScreenModeText()
    {
        if (fullScreenModeName != null)
            fullScreenModeName.StringChanged -= OnFullScreenModeChanged;

        string currentFullScreenMode = graphicsSettings.CurrentFullScreenMode.ToString().ToLower();

        fullScreenModeName = new LocalizedString
        {
            TableReference = SETTINGS_TABLE_REFERENCE,
            TableEntryReference = $"{FULLSCREEN_MODES_ENTRY_REFERENCE}{currentFullScreenMode}"
        };

        fullScreenModeName.StringChanged += OnFullScreenModeChanged;
        fullScreenModeName.RefreshString();
    }

    private void OnFullScreenModeChanged(string value) => fullScreenModeText.text = value;
}