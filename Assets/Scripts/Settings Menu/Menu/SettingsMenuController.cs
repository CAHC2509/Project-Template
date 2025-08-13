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

    private IViewBase view;

    private void Awake()
    {
        view = GetComponentInChildren<IViewBase>();
        inputSettings.Dependencies();
        localizationSettings.Dependencies();
    }

    public override void Initialize()
    {
        base.Initialize();

        view.Initialize();

        graphicsSettings.Initialize();
        audioSettings.Initialize();
        inputSettings.Initialize();
        localizationSettings.Initialize();

        AddListeners();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();

        graphicsSettings.Conclude();
        audioSettings.Conclude();
        inputSettings.Conclude();
        localizationSettings.Conclude();

        RemoveListeners();
    }

    public void OpenSettingsView()
    {
        view.EnableView();
    }

    public void CloseSettingsView()
    {
        view.DisableView();
        OnSettingsClosed?.Invoke();
    }
}
