using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuView : ViewBase, ISettingsMenuView
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

    private ISettingsMenuController menuController;
    private SelectableStateController lastRootSelectable;
    private GameObject currentPanel;
    private bool currentSelectableIsRoot;

    private void Awake()
    {
        menuController = GetComponentInParent<ISettingsMenuController>();
        defaultSelection = graphicsButton.Button;
    }

    protected override void AddTemporaryListeners() => UIInputManager.OnCancel += OnCancelPressed;
    protected override void RemoveTemporaryListeners() => UIInputManager.OnCancel -= OnCancelPressed;

    protected override void AddPersistentListeners()
    {
        graphicsButton.Button.onClick.AddListener(SetGraphicsPanel);
        audioButton.Button.onClick.AddListener(SetAudioPanel);
        controlsButton.Button.onClick.AddListener(SetControlsPanel);
        languageButton.Button.onClick.AddListener(SetLanguagePanel);
        closeSettingsButton.onClick.AddListener(CloseSettingsFromButton);
    }

    protected override void RemovePersistentListeners()
    {
        graphicsButton.Button.onClick.RemoveListener(SetGraphicsPanel);
        audioButton.Button.onClick.RemoveListener(SetAudioPanel);
        controlsButton.Button.onClick.RemoveListener(SetControlsPanel);
        languageButton.Button.onClick.RemoveListener(SetLanguagePanel);
        closeSettingsButton.onClick.RemoveListener(CloseSettingsFromButton);
    }

    public override void EnableView()
    {
        base.EnableView();

        SetGraphicsPanel();
    }

    private void CloseSettingsFromButton()
    {
        if (rebindWindow.activeSelf || invalidRebindWindow.activeSelf)
            return;

        menuController.CloseSettingsView();
    }

    private void OnCancelPressed()
    {
        if (rebindWindow.activeSelf || invalidRebindWindow.activeSelf)
            return;

        if (!currentSelectableIsRoot)
        {
            SetRootSelectable();
            return;
        }

        menuController.CloseSettingsView();
    }

    private void UpdateCurrentPanel(GameObject newPanel, SelectableStateController newSelectable, SelectableStateController rootSelectable)
    {
        if (currentPanel != null)
            currentPanel.SetActive(false);

        currentPanel = newPanel;
        newPanel.SetActive(true);

        lastRootSelectable = rootSelectable;

        StartCoroutine(SelectionWithDelay(newSelectable));
    }

    private IEnumerator SelectionWithDelay(SelectableStateController newSelectable)
    {
        yield return new WaitUntil(() => currentPanel.activeSelf);
        SelectionManager.SetNewSelectable?.Invoke(newSelectable);
        currentSelectableIsRoot = false;
    }

    private void SetRootSelectable()
    {
        SelectionManager.SetNewSelectable?.Invoke(lastRootSelectable);
        currentSelectableIsRoot = true;
    }

    private void SetGraphicsPanel() => UpdateCurrentPanel(graphicsPanel, graphicsDefaultSelection, graphicsButton);
    private void SetAudioPanel() => UpdateCurrentPanel(audioPanel, audioDefaultSelection, audioButton);
    private void SetControlsPanel() => UpdateCurrentPanel(controlsPanel, controlsDefaultSelection, controlsButton);
    private void SetLanguagePanel() => UpdateCurrentPanel(languagePanel, languageDefaultSelection, languageButton);
}
