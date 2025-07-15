using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuView : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject rebindWindow;
    [SerializeField] private GameObject invalidRebindWindow;
    [SerializeField] private Button closeSettingsButton;

    [Space, Header("Graphics")]
    [SerializeField] private SelectableStateController graphicsButton;
    [SerializeField] private GameObject graphicsPanel;
    [SerializeField] private SelectableStateController graphicsDefaultSelection;

    [Space, Header("Audio")]
    [SerializeField] private SelectableStateController audioButton;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private SelectableStateController audioDefaultSelection;

    [Space, Header("Controls")]
    [SerializeField] private SelectableStateController controlsButton;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private SelectableStateController controlsDefaultSelection;

    [Space, Header("Controls")]
    [SerializeField] private SelectableStateController languageButton;
    [SerializeField] private GameObject languagePanel;
    [SerializeField] private SelectableStateController languageDefaultSelection;

    private SettingsMenuEntity menuSettings;

    public static event Action OnSettingsMenuClose;

    public void Initialize(SettingsMenuEntity menuSettings)
    {
        this.menuSettings = menuSettings;
        AddListeners();
    }

    private void AddListeners()
    {
        graphicsButton.Button.onClick.AddListener(SetGraphicsPanel);
        audioButton.Button.onClick.AddListener(SetAudioPanel);
        controlsButton.Button.onClick.AddListener(SetControlsPanel);
        languageButton.Button.onClick.AddListener(SetLanguagePanel);
        closeSettingsButton.onClick.AddListener(CloseSettingsFromButton);

        MainMenuView.OnSettingsPressed += OpenSettingsMenu;
        UIInputManager.OnCancel += CloseSettingsFromCancelInput;
        UIInputManager.OnCancel += GoToRootOptionsMenu;

        menuSettings.OnNewPanelSelected += UpdateCurrentPanel;
    }

    private void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        SetDefaultPanel();
    }

    private void CloseSettingsFromCancelInput()
    {
        if (rebindWindow.activeSelf || invalidRebindWindow.activeSelf)
            return;

        if (!menuSettings.CurrentSelectableIsRoot)
            return;

        settingsMenu.SetActive(false);
        OnSettingsMenuClose?.Invoke();
    }

    private void CloseSettingsFromButton()
    {
        if (rebindWindow.activeSelf || invalidRebindWindow.activeSelf)
            return;

        settingsMenu.SetActive(false);
        OnSettingsMenuClose?.Invoke();
    }

    private void GoToRootOptionsMenu()
    {
        if (rebindWindow.activeSelf || invalidRebindWindow.activeSelf)
            return;

        menuSettings.ExitFromCurrentPanel();
    }

    private void UpdateCurrentPanel()
    {
        StartCoroutine(DelayedSelection());
    }

    private IEnumerator DelayedSelection()
    {
        menuSettings.CurrentPanelSelected.SetActive(true);
        yield return new WaitUntil(() => menuSettings.CurrentPanelSelected.gameObject.activeInHierarchy);
        SelectionManager.Instance.Select(menuSettings.CurrentSelectable);
    }

    private void SetDefaultPanel() => SetGraphicsPanel();
    private void SetGraphicsPanel() => menuSettings.SelectNewPanel(graphicsButton, graphicsDefaultSelection, graphicsPanel);
    private void SetAudioPanel() => menuSettings.SelectNewPanel(audioButton, audioDefaultSelection, audioPanel);
    private void SetControlsPanel() => menuSettings.SelectNewPanel(controlsButton, controlsDefaultSelection, controlsPanel);
    private void SetLanguagePanel() => menuSettings.SelectNewPanel(languageButton, languageDefaultSelection, languagePanel);
}
