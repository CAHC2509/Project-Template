using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class InputSettingsView : ViewBase, IInputSettingsView
{
    [Header("Multiplatform")]
    [SerializeField] private Button keyboardControlsButton;
    [SerializeField] private GameObject keyboardControlsPanel;
    [SerializeField] private Button gamepadControlsButton;
    [SerializeField] private GameObject gamepadControlsPanel;

    [Space, Header("Rebind")]
    [SerializeField] private Button resetRebindsButton;
    [SerializeField] private GameObject rebindWindow;
    [SerializeField] private LocalizeStringEvent rebindMessageLocalized;

    [Space, Header("Invalid rebind")]
    [SerializeField] private GameObject invalidRebindWindow;

    private IInputSettingsController controller;

    private void Awake()
    {
        controller = GetComponentInParent<IInputSettingsController>();
        defaultSelection = keyboardControlsButton;
    }

    public override void Initialize()
    {
        base.Initialize();

        OpenKeyboardControlsPanel();
    }

    protected override void AddPersistentListeners()
    {
        resetRebindsButton.onClick.AddListener(controller.ResetToDefaults);

        keyboardControlsButton.onClick.AddListener(OpenKeyboardControlsPanel);
        gamepadControlsButton.onClick.AddListener(OpenGamepadControlsPanel);

        UIInputManager.OnCancel += HideRebindWindow;
        UIInputManager.OnCancel += HideInvalidRebindWindow;
    }

    protected override void RemovePersistentListeners()
    {
        resetRebindsButton.onClick.RemoveListener(controller.ResetToDefaults);

        keyboardControlsButton.onClick.RemoveListener(OpenKeyboardControlsPanel);
        gamepadControlsButton.onClick.RemoveListener(OpenGamepadControlsPanel);

        UIInputManager.OnCancel -= HideRebindWindow;
        UIInputManager.OnCancel -= HideInvalidRebindWindow;
    }

    private void OpenKeyboardControlsPanel()
    {
        gamepadControlsPanel.SetActive(false);
        keyboardControlsPanel.SetActive(true);
    }

    private void OpenGamepadControlsPanel()
    {
        keyboardControlsPanel.SetActive(false);
        gamepadControlsPanel.SetActive(true);
    }

    public void ShowRebindWindow()
    {
        string displayName = controller.GetModel().InputActionToRebindName;
        string displayKey = controller.GetModel().InputActionToRebindKey;

        rebindMessageLocalized.StringReference["displayName"] = new StringVariable { Value = displayName };
        rebindMessageLocalized.StringReference["displayKey"] = new StringVariable { Value = displayKey };

        rebindMessageLocalized.RefreshString();
        rebindWindow.SetActive(true);
    }

    public void HideRebindWindow()
    {
        rebindWindow.SetActive(false);
    }

    public void ShowInvalidRebindWindow()
    {
        invalidRebindWindow.SetActive(true);
    }

    private void HideInvalidRebindWindow()
    {
        invalidRebindWindow.SetActive(false);
    }
}
