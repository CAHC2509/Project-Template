using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuView : ViewBase, ISettingsMenuView
{
    [Header("Main")]
    [SerializeField] private GameObject rebindWindow;
    [SerializeField] private GameObject invalidRebindWindow;
    [SerializeField] private Button closeSettingsButton;

    [Space, Header("Submenus")]
    [SerializeField] private SelectableStateController graphicsButton;
    [SerializeField] private SelectableStateController audioButton;
    [SerializeField] private SelectableStateController controlsButton;
    [SerializeField] private SelectableStateController languageButton;

    private ISettingsMenuController controller;
    private SelectableStateController lastRootSelectable;
    private bool currentSelectableIsRoot;

    private void Awake()
    {
        controller = GetComponentInParent<ISettingsMenuController>();
        defaultSelection = graphicsButton.Button;
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

        controller.CloseMenuView();
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

        controller.CloseMenuView();
    }

    private void UpdateCurrentPanel(SettingsSubmenuType submenuType, SelectableStateController rootSelectable)
    {
        controller.ChangeCurrentPanel(submenuType);
        lastRootSelectable = rootSelectable;
        currentSelectableIsRoot = false;
    }

    private void SetRootSelectable()
    {
        SelectionManager.SetNewSelectable?.Invoke(lastRootSelectable);
        currentSelectableIsRoot = true;
    }

    private void SetGraphicsPanel() => UpdateCurrentPanel(SettingsSubmenuType.Graphics, graphicsButton);
    private void SetAudioPanel() => UpdateCurrentPanel(SettingsSubmenuType.Audio, audioButton);
    private void SetControlsPanel() => UpdateCurrentPanel(SettingsSubmenuType.Input, controlsButton);
    private void SetLanguagePanel() => UpdateCurrentPanel(SettingsSubmenuType.Localization, languageButton);
}
