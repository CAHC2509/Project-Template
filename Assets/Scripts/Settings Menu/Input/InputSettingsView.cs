using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class InputSettingsView : MonoBehaviour
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

    private InputSettingsEntity inputSettings;

    public void Initialize(InputSettingsEntity inputSettings)
    {
        this.inputSettings = inputSettings;
        AddListeners();
        OpenKeyboardControlsPanel();
    }

    public void Conclude()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        resetRebindsButton.onClick.AddListener(TriggerRebindsReset);

        keyboardControlsButton.onClick.AddListener(OpenKeyboardControlsPanel);
        gamepadControlsButton.onClick.AddListener(OpenGamepadControlsPanel);

        inputSettings.OnRebindRequest += ShowRebindWindow;
        inputSettings.OnRebindComplete += HideRebindWindow;
        inputSettings.OnRebindInvalid += HideRebindWindow;
        inputSettings.OnRebindInvalid += ShowInvalidRebindWindow;
        inputSettings.OnRebindCancel += HideRebindWindow;

        UIInputManager.OnCancel += HideRebindWindow;
        UIInputManager.OnCancel += HideInvalidRebindWindow;
    }

    private void RemoveListeners()
    {
        resetRebindsButton.onClick.RemoveListener(TriggerRebindsReset);

        keyboardControlsButton.onClick.RemoveListener(OpenKeyboardControlsPanel);
        gamepadControlsButton.onClick.RemoveListener(OpenGamepadControlsPanel);

        inputSettings.OnRebindRequest -= ShowRebindWindow;
        inputSettings.OnRebindComplete -= HideRebindWindow;
        inputSettings.OnRebindInvalid -= HideRebindWindow;
        inputSettings.OnRebindInvalid -= ShowInvalidRebindWindow;
        inputSettings.OnRebindCancel -= HideRebindWindow;

        UIInputManager.OnCancel -= HideRebindWindow;
        UIInputManager.OnCancel -= HideInvalidRebindWindow;
    }

    private void ShowRebindWindow()
    {
        string displayName = inputSettings.InputActionToRebindName;
        string displayKey = inputSettings.InputActionToRebindKey;

        rebindMessageLocalized.StringReference["displayName"] = new StringVariable { Value = displayName };
        rebindMessageLocalized.StringReference["displayKey"] = new StringVariable { Value = displayKey };

        rebindMessageLocalized.RefreshString();
        rebindWindow.SetActive(true);
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

    private void HideRebindWindow() => rebindWindow.SetActive(false);
    private void ShowInvalidRebindWindow() => invalidRebindWindow.SetActive(true);
    private void HideInvalidRebindWindow() => invalidRebindWindow.SetActive(false);
    private void TriggerRebindsReset() => inputSettings.ResetRebinds();
}
