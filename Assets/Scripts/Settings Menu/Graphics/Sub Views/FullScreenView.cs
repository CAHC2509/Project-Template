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

    private IGraphicsSettingsController controller;
    private LocalizedString fullScreenModeName;

    public void Initialize(IGraphicsSettingsController controller)
    {
        this.controller = controller;

        previousFullScreenButton.onClick.AddListener(OnDisableFullScreenMode);
        nextFullScreenButton.onClick.AddListener(OnEnableFullScreenMode);

        fullScreenListController.OnPreviousItemRequested += OnDisableFullScreenMode;
        fullScreenListController.OnNextItemRequested += OnEnableFullScreenMode;

        InitializeSelectors();
    }

    public void Conclude()
    {
        previousFullScreenButton.onClick.RemoveListener(OnDisableFullScreenMode);
        nextFullScreenButton.onClick.RemoveListener(OnEnableFullScreenMode);

        fullScreenListController.OnPreviousItemRequested -= OnDisableFullScreenMode;
        fullScreenListController.OnNextItemRequested -= OnEnableFullScreenMode;

        if (fullScreenModeName != null)
            fullScreenModeName.StringChanged -= OnFullScreenModeChanged;
    }

    private void InitializeSelectors()
    {
        UpdateFullScreenModeButtons();
        UpdateFullScreenModeText();
    }

    private void OnDisableFullScreenMode()
    {
        controller.SetFullScreenMode(false);
    }

    private void OnEnableFullScreenMode()
    {
        controller.SetFullScreenMode(true);
    }

    public void UpdateFullScreenModeButtons()
    {
        bool activeMode = controller.GetModel().CurrentFullScreenMode;
        previousFullScreenButton.gameObject.SetActive(activeMode);
        nextFullScreenButton.gameObject.SetActive(!activeMode);
    }

    public void UpdateFullScreenModeText()
    {
        if (fullScreenModeName != null)
            fullScreenModeName.StringChanged -= OnFullScreenModeChanged;

        string currentFullScreenMode = controller.GetModel().CurrentFullScreenMode.ToString().ToLower();

        fullScreenModeName = new LocalizedString
        {
            TableReference = SETTINGS_TABLE_REFERENCE,
            TableEntryReference = $"{FULLSCREEN_MODES_ENTRY_REFERENCE}{currentFullScreenMode}"
        };

        fullScreenModeName.StringChanged += OnFullScreenModeChanged;
        fullScreenModeName.RefreshString();
    }

    private void OnFullScreenModeChanged(string value)
    {
        fullScreenModeText.text = value;
    }
}