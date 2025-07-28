using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSettingsController : MonoBehaviour, ISettings
{
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private InputSettingsView view;
    [SerializeField] private Transform inputActionsKeyboardParent;
    [SerializeField] private Transform inputActionsGamepadParent;

    private const string INPUT_ACTIONS_KEY = "InputActions";

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private List<IndividualInputRebinder> inputRebindersKeyboard = new List<IndividualInputRebinder>();
    private List<IndividualInputRebinder> inputRebindersGamepad = new List<IndividualInputRebinder>();
    private InputSettingsEntity inputSettings;

    public void Initialize()
    {
        LoadSettings();
        AddListeners();
        GetInputRebinders();
        InitializeInputRebinders();
        view.Initialize(inputSettings);
    }

    public void Conclude()
    {
        RemoveListeners();
        view.Conclude();
    }

    private void AddListeners()
    {
        inputSettings.OnRebindRequest += PerformRebind;
        inputSettings.OnRebindComplete += SaveSettings;
        inputSettings.OnRebindsReset += ResetToDefaults;
    }

    private void RemoveListeners()
    {
        inputSettings.OnRebindRequest -= PerformRebind;
        inputSettings.OnRebindComplete -= SaveSettings;
        inputSettings.OnRebindsReset -= ResetToDefaults;
    }

    private void GetInputRebinders()
    {
        inputRebindersKeyboard.Clear();
        inputRebindersGamepad.Clear();

        inputRebindersKeyboard = inputActionsKeyboardParent.GetComponentsInChildren<IndividualInputRebinder>().ToList();
        inputRebindersGamepad = inputActionsGamepadParent.GetComponentsInChildren<IndividualInputRebinder>().ToList();
    }

    private void InitializeInputRebinders()
    {
        foreach (var rebinder in inputRebindersKeyboard)
            rebinder.Initialize(inputSettings);

        foreach (var rebinder in inputRebindersGamepad)
            rebinder.Initialize(inputSettings);
    }

    private void PerformRebind()
    {
        var actionToRebind = inputSettings.InputActionToRebind.action;
        var actionMap = actionToRebind.actionMap;

        int bindingIndex = FindBindingIndexForDeviceType(actionToRebind, inputSettings.DeviceTypeToRebind);

        if (bindingIndex == -1)
        {
            Debug.LogWarning("No binding found for the specified device type.");
            return;
        }

        actionMap.Disable();

        rebindingOperation = actionToRebind.PerformInteractiveRebinding(bindingIndex)
            .WithCancelingThrough("*/{Cancel}")
            .OnComplete(operation =>
            {
                string newBindingPath = actionToRebind.bindings[bindingIndex].effectivePath;

                bool alreadyInUse = inputActionAsset.actionMaps
                    .SelectMany(map => map.actions)
                    .Where(otherAction => otherAction != actionToRebind)
                    .SelectMany(otherAction => otherAction.bindings)
                    .Where(binding => !binding.isComposite && !binding.isPartOfComposite)
                    .Select(binding => string.IsNullOrEmpty(binding.overridePath) ? binding.path : binding.overridePath)
                    .Where(path => !string.IsNullOrEmpty(path))
                    .Any(path => path == newBindingPath);

                if (alreadyInUse)
                {
                    inputSettings.InvalidateCurrentRebindRequest();
                    actionToRebind.RemoveBindingOverride(bindingIndex);
                }
                else
                {
                    SaveSettings();
                    inputSettings.CompleteCurrentRebindRequest();
                }

                actionMap.Enable();
                rebindingOperation.Dispose();
                rebindingOperation = null;
            })
            .OnCancel(operation =>
            {
                actionMap.Enable();
                rebindingOperation.Dispose();
                rebindingOperation = null;
                inputSettings.CancelCurrentRebindRequest();
            })
            .Start();
    }

    private int FindBindingIndexForDeviceType(InputAction action, InputSettingsEntity.InputDeviceType deviceType)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            var binding = action.bindings[i];
            if (binding.isComposite || binding.isPartOfComposite)
                continue;

            string path = binding.effectivePath.ToLower();

            if (deviceType == InputSettingsEntity.InputDeviceType.Gamepad && path.Contains("gamepad"))
                return i;

            if (deviceType == InputSettingsEntity.InputDeviceType.KeyboardMouse &&
                (path.Contains("keyboard") || path.Contains("mouse")))
                return i;
        }

        return -1;
    }

    private void ResetToDefaults()
    {
        inputActionAsset.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(INPUT_ACTIONS_KEY);
        PlayerPrefs.Save();
        LoadSettings();
        InitializeInputRebinders();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(INPUT_ACTIONS_KEY))
        {
            string rebinds = PlayerPrefs.GetString(INPUT_ACTIONS_KEY);
            inputActionAsset.LoadBindingOverridesFromJson(rebinds);
        }
        else
        {
            inputActionAsset.RemoveAllBindingOverrides();
        }

        inputSettings = new InputSettingsEntity();
    }

    public void SaveSettings()
    {
        string rebinds = inputActionAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(INPUT_ACTIONS_KEY, rebinds);
        PlayerPrefs.Save();
    }
}
