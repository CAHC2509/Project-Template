using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using TMPro;

public class InputSettingsView : MonoBehaviour
{
    [Header("Rebind")]
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
    }

    private void AddListeners()
    {
        resetRebindsButton.onClick.AddListener(TriggerRebindsReset);

        inputSettings.OnRebindRequest += ShowRebindWindow;
        inputSettings.OnRebindComplete += HideRebindWindow;
        inputSettings.OnRebindInvalid += HideRebindWindow;
        inputSettings.OnRebindInvalid += ShowInvalidRebindWindow;
        inputSettings.OnRebindCancel += HideRebindWindow;
        UIInputManager.OnCancel += HideRebindWindow;
        UIInputManager.OnCancel += HideInvalidRebindWindow;
    }

    private void ShowRebindWindow()
    {
        var bindings = inputSettings.InputActionToRebind.action.bindings;
        string displayKey = InputControlPath.ToHumanReadableString(
            bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice
        );

        string displayName = inputSettings.InputActionToRebindName;

        rebindMessageLocalized.StringReference["displayName"] = new StringVariable { Value = displayName };
        rebindMessageLocalized.StringReference["displayKey"] = new StringVariable { Value = displayKey };

        rebindMessageLocalized.RefreshString();
        rebindWindow.SetActive(true);
    }

    private void HideRebindWindow() => rebindWindow.SetActive(false);

    private void ShowInvalidRebindWindow() => invalidRebindWindow.SetActive(true);

    private void HideInvalidRebindWindow() => invalidRebindWindow.SetActive(false);

    private void TriggerRebindsReset() => inputSettings.ResetRebinds();
}
