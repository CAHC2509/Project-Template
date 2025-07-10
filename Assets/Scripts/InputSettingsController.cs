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
        inputSettings.OnRebindRequest += StartRebind;
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

    private void StartRebind()
    {
        int bindingIndex = 0;

        var actionToRebind = inputSettings.InputActionToRebind.action;
        actionToRebind.Disable();

        rebindingOperation = actionToRebind.PerformInteractiveRebinding(bindingIndex)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation =>
            {
                string newBindingPath = actionToRebind.bindings[bindingIndex].effectivePath;

                bool alreadyInUse = inputActionAsset.actionMaps
                    .SelectMany(map => map.actions)
                    .Any(action =>
                        action != actionToRebind &&
                        action.bindings.Any(b => b.effectivePath == newBindingPath)
                    );

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

                actionToRebind.Enable();
                operation.Dispose();
            })
            .OnCancel(operation =>
            {
                actionToRebind.Enable();
                operation.Dispose();
                inputSettings.CancelCurrentRebindRequest();
            })
            .Start();
    }

    private void ResetToDefaults()
    {
        inputActionAsset.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(INPUT_ACTIONS_KEY);
        PlayerPrefs.Save();
    }


    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(INPUT_ACTIONS_KEY))
        {
            string rebinds = PlayerPrefs.GetString(INPUT_ACTIONS_KEY);
            inputActionAsset.LoadBindingOverridesFromJson(rebinds);
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
