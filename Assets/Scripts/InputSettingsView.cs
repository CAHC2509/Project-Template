using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InputSettingsView : MonoBehaviour
{
    [SerializeField] private Button resetRebindsButton;
    [SerializeField] private GameObject rebindWindow;
    [SerializeField] private TextMeshProUGUI rebindText;

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
        inputSettings.OnRebindInvalid += ShowInvalidRebindWindow;
        inputSettings.OnRebindCancel += HideRebindWindow;
        UIInputController.OnCancel += HideRebindWindow;
    }

    private void ShowRebindWindow()
    {
        var bindings = inputSettings.InputActionToRebind.action.bindings;
        string displayKey = InputControlPath.ToHumanReadableString(
                    bindings[0].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice
                );

        string displayName = inputSettings.InputActionToRebind.action.name;

        rebindText.text = $"Select a new key to {displayName}, current key is {displayKey}";
        rebindWindow.SetActive(true);
    }

    private void ShowInvalidRebindWindow()
    {
        rebindWindow.SetActive(false);
        rebindText.text = $"The key you pressed is already in use, select a different one";
        rebindWindow.SetActive(true);
    }

    private void TriggerRebindsReset() => inputSettings.ResetRebinds();

    private void HideRebindWindow() => rebindWindow.SetActive(false);
}
