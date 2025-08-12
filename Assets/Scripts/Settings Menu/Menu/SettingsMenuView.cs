using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuView : ViewBase
{
    [Header("Main")]
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

    [Space, Header("Language")]
    [SerializeField] private SelectableStateController languageButton;
    [SerializeField] private GameObject languagePanel;
    [SerializeField] private SelectableStateController languageDefaultSelection;

    private SettingsMenuEntity menuSettings;

    public void Dependencies(SettingsMenuEntity menuSettings)
    {
        this.menuSettings = menuSettings;
        defaultSelection = graphicsButton.Button;
    }
    
    public override void Initialize()
    {
        AddPersistentListeners();
    }

    public override void Conclude()
    {
        RemovePersistentListeners();
    }

    protected override void AddTemporaryListeners()
    {
        UIInputManager.OnCancel += OnCancelPressed;
    }

    protected override void RemoveTemporaryListeners()
    {
        UIInputManager.OnCancel -= OnCancelPressed;
    }

    protected override void AddPersistentListeners()
    {
        graphicsButton.Button.onClick.AddListener(SetGraphicsPanel);
        audioButton.Button.onClick.AddListener(SetAudioPanel);
        controlsButton.Button.onClick.AddListener(SetControlsPanel);
        languageButton.Button.onClick.AddListener(SetLanguagePanel);
        closeSettingsButton.onClick.AddListener(CloseSettingsFromButton);

        menuSettings.OnPanelChanged += UpdateCurrentPanel;
        menuSettings.OnSettingsMenuClosed += DisableView;
    }

    protected override void RemovePersistentListeners()
    {
        graphicsButton.Button.onClick.RemoveListener(SetGraphicsPanel);
        audioButton.Button.onClick.RemoveListener(SetAudioPanel);
        controlsButton.Button.onClick.RemoveListener(SetControlsPanel);
        languageButton.Button.onClick.RemoveListener(SetLanguagePanel);
        closeSettingsButton.onClick.RemoveListener(CloseSettingsFromButton);

        menuSettings.OnPanelChanged -= UpdateCurrentPanel;
        menuSettings.OnSettingsMenuClosed -= DisableView;
    }

    public override void EnableView()
    {
        base.EnableView();

        SetDefaultPanel();
    }

    private void CloseSettingsFromButton()
    {
        if (rebindWindow.activeSelf || invalidRebindWindow.activeSelf)
            return;

        menuSettings.CloseSettingsMenu();
    }

    private void OnCancelPressed()
    {
        if (rebindWindow.activeSelf || invalidRebindWindow.activeSelf)
            return;

        if (!menuSettings.CurrentSelectableIsRoot)
        {
            menuSettings.SetRootSelectable();
            return;
        }

        menuSettings.CloseSettingsMenu();
    }

    private void UpdateCurrentPanel()
    {
        if (menuSettings.PreviousPanelSelected != null)
            menuSettings.PreviousPanelSelected.SetActive(false);

        menuSettings.CurrentPanelSelected.SetActive(true);

        StartCoroutine(SelectionWithDelay());
    }

    private IEnumerator SelectionWithDelay()
    {
        yield return new WaitUntil(() => menuSettings.CurrentPanelSelected.activeInHierarchy);
        SelectionManager.SetNewSelectable?.Invoke(menuSettings.CurrentSelectable);
    }

    private void SetDefaultPanel() => SetGraphicsPanel();
    private void SetGraphicsPanel() => menuSettings.SetCurrentPanel(graphicsButton, graphicsDefaultSelection, graphicsPanel);
    private void SetAudioPanel() => menuSettings.SetCurrentPanel(audioButton, audioDefaultSelection, audioPanel);
    private void SetControlsPanel() => menuSettings.SetCurrentPanel(controlsButton, controlsDefaultSelection, controlsPanel);
    private void SetLanguagePanel() => menuSettings.SetCurrentPanel(languageButton, languageDefaultSelection, languagePanel);
}
