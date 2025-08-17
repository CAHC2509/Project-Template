using System;
using UnityEngine;

public class SettingsMenuController : ControllerBase, ISettingsMenuController
{    
    [Header("Settings modules")]
    [SerializeField] private GraphicsSettingsController graphicsSettings;
    [SerializeField] private AudioSettingsController audioSettings;
    [SerializeField] private InputSettingsController inputSettings;
    [SerializeField] private LocalizationSettingsController localizationSettings;

    public event Action OnSettingsClosed;

    private ISettingsMenuView settingsView;
    private IDescriptionMessageView descriptionView;

    private void Awake()
    {
        settingsView = GetComponentInChildren<ISettingsMenuView>();
        descriptionView = GetComponentInChildren<IDescriptionMessageView>();
    }

    public override void Initialize()
    {
        base.Initialize();

        settingsView.Initialize();
        descriptionView.Initialize();

        graphicsSettings.Initialize();
        audioSettings.Initialize();
        inputSettings.Initialize();
        localizationSettings.Initialize();

        AddListeners();
    }

    public override void Conclude()
    {
        base.Conclude();

        settingsView.Conclude();
        descriptionView.Conclude();

        graphicsSettings.Conclude();
        audioSettings.Conclude();
        inputSettings.Conclude();
        localizationSettings.Conclude();

        RemoveListeners();
    }

    public void OpenSettingsView()
    {
        settingsView.EnableView();
    }

    public void CloseSettingsView()
    {
        settingsView.DisableView();
        OnSettingsClosed?.Invoke();
    }
}
