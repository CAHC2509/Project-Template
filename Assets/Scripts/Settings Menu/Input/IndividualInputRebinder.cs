using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Button))]
public class IndividualInputRebinder : MonoBehaviour
{
    [SerializeField] private InputActionReference assignedInput;
    [SerializeField] private TextMeshProUGUI inputNameText;
    [SerializeField] private TextMeshProUGUI inputBindingText;
    [SerializeField] private InputSettingsEntity.InputDeviceType deviceType;

    private InputSettingsEntity inputSettings;
    private Button assignedButton;

    public void Initialize(InputSettingsEntity inputSettings)
    {
        this.inputSettings = inputSettings;
        assignedButton = GetComponent<Button>();

        AddListeners();
        UpdateText();
    }

    private void AddListeners()
    {
        assignedButton.onClick.AddListener(RebindRequest);
        inputSettings.OnRebindComplete += UpdateText;
        inputSettings.OnRebindsReset += UpdateText;
    }

    private void UpdateText()
    {
        var bindings = assignedInput.action.bindings;

        for (int i = 0; i < bindings.Count; i++)
        {
            if (bindings[i].isComposite || bindings[i].isPartOfComposite)
                continue;

            string path = bindings[i].effectivePath;

            if (string.IsNullOrEmpty(path))
                continue;

            bool isGamepad = path.Contains("Gamepad");
            bool isKeyboard = path.Contains("Keyboard") || path.Contains("Mouse");

            if ((deviceType == InputSettingsEntity.InputDeviceType.Gamepad && isGamepad) ||
                (deviceType == InputSettingsEntity.InputDeviceType.KeyboardMouse && isKeyboard))
            {
                string displayString = InputControlPath.ToHumanReadableString(
                    path,
                    InputControlPath.HumanReadableStringOptions.OmitDevice
                );

                inputBindingText.text = displayString;
                return;
            }
        }

        inputBindingText.text = "----";
    }


    private void RebindRequest() => inputSettings.CreateNewRebindRequest(assignedInput, inputNameText.text, inputBindingText.text, deviceType);
}
