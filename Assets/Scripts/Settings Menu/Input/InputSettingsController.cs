using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSettingsController : ControllerBase, IInputSettingsController
{
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private Transform inputActionsKeyboardParent;
    [SerializeField] private Transform inputActionsGamepadParent;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private List<IndividualInputRebinder> inputRebindersKeyboard = new List<IndividualInputRebinder>();
    private List<IndividualInputRebinder> inputRebindersGamepad = new List<IndividualInputRebinder>();

    private ISaveSystem saveSystem;
    private IInputSettingsView view;
    private InputSettingsEntity inputSettings;

    private void Awake()
    {
        view = GetComponentInChildren<IInputSettingsView>();
        inputSettings = new InputSettingsEntity();
    }

    public void Dependencies(ISaveSystem saveSystem)
    {
        this.saveSystem = saveSystem;
    }

    public override void Initialize()
    {
        base.Initialize();

        LoadSettings();
        GetInputRebinders();
        InitializeInputRebinders();
        view.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
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
            rebinder.Initialize(this);

        foreach (var rebinder in inputRebindersGamepad)
            rebinder.Initialize(this);
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
                    inputSettings.ClearReferences();
                    actionToRebind.RemoveBindingOverride(bindingIndex);
                    view.ShowInvalidRebindWindow();
                }
                else
                {
                    inputSettings.CurrentRebinder.UpdateText();
                    SaveSettings();
                    inputSettings.ClearReferences();
                }

                actionMap.Enable();
                rebindingOperation.Dispose();
                rebindingOperation = null;

                view.HideRebindWindow();
            })
            .OnCancel(operation =>
            {
                actionMap.Enable();
                rebindingOperation.Dispose();
                rebindingOperation = null;
                inputSettings.ClearReferences();

                view.HideRebindWindow();
            })
            .Start();
    }

    private int FindBindingIndexForDeviceType(InputAction action, InputDeviceType deviceType)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            var binding = action.bindings[i];
            if (binding.isComposite || binding.isPartOfComposite)
                continue;

            string path = binding.effectivePath.ToLower();

            if (deviceType == InputDeviceType.Gamepad && path.Contains("gamepad"))
                return i;

            if (deviceType == InputDeviceType.KeyboardMouse &&
                (path.Contains("keyboard") || path.Contains("mouse")))
                return i;
        }

        return -1;
    }

    public void ResetToDefaults()
    {
        inputActionAsset.RemoveAllBindingOverrides();
        saveSystem.Delete(Constants.INPUT_ACTIONS_KEY);
        LoadSettings();
        InitializeInputRebinders();
    }

    public void LoadSettings()
    {
        if (saveSystem.HasKey(Constants.INPUT_ACTIONS_KEY))
        {
            StringWrapper rebinds = (StringWrapper)saveSystem.Load(Constants.INPUT_ACTIONS_KEY, typeof(StringWrapper));
            inputActionAsset.LoadBindingOverridesFromJson(rebinds.value);
        }
        else
        {
            inputActionAsset.RemoveAllBindingOverrides();
        }
    }

    public void SaveSettings()
    {
        string rebinds = inputActionAsset.SaveBindingOverridesAsJson();
        saveSystem.Save(Constants.INPUT_ACTIONS_KEY, new StringWrapper(rebinds));
    }

    public void CreateNewRebindRequest(RebindRequestData rebindRequestData)
    {
        inputSettings.CreateNewRebindRequest(rebindRequestData);
        view.ShowRebindWindow();
        PerformRebind();
    }

    public InputSettingsEntity GetModel()
    {
        return inputSettings;
    }

    public void EnableView()
    {
        view.EnableView();
    }

    public void DisableView()
    {
        view.DisableView();
    }
}
