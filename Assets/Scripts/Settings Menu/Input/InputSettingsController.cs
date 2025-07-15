using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSettingsController : MonoBehaviour, ISettings
{
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private InputSettingsView view;
    [SerializeField] private Transform inputActionsParent;

    private const string INPUT_ACTIONS_KEY = "InputActions";

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private List<IndividualInputRebinder> inputRebinders = new List<IndividualInputRebinder>();
    private InputSettingsEntity inputSettings;

    private void Awake() => GameManager.OnInitialization += OnInitialization;

    private void OnInitialization()
    {
        LoadSettings();
        AddListeners();
        GetInputRebinders();
        InitializeInputRebinders();
        view.Initialize(inputSettings);
    }

    private void AddListeners()
    {
        inputSettings.OnRebindRequest += PerformRebind;
        inputSettings.OnRebindComplete += SaveSettings;
        inputSettings.OnRebindsReset += ResetToDefaults;
    }

    private void GetInputRebinders()
    {
        inputRebinders.Clear();
        inputRebinders = inputActionsParent.GetComponentsInChildren<IndividualInputRebinder>().ToList();
    }

    private void InitializeInputRebinders()
    {
        foreach (IndividualInputRebinder rebinder in inputRebinders)
            rebinder.Initialize(inputSettings);
    }

    private void PerformRebind()
    {
        int bindingIndex = 0;

        var actionToRebind = inputSettings.InputActionToRebind.action;
        var actionMap = actionToRebind.actionMap;

        actionMap.Disable();

        rebindingOperation = actionToRebind.PerformInteractiveRebinding(bindingIndex)
            .WithCancelingThrough("<Keyboard>/escape")
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