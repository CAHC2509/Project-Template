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
            if (!bindings[i].isComposite && !bindings[i].isPartOfComposite)
            {
                string displayString = InputControlPath.ToHumanReadableString(
                    bindings[i].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice
                );

                inputBindingText.text = displayString;
                return;
            }
        }

        inputBindingText.text = string.Empty;
    }

    private void RebindRequest() => inputSettings.CreateNewRebindRequest(assignedInput, inputNameText.text);
}
