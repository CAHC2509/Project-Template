using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private SettingsMenuController settingsController;
    [SerializeField] private GraphicsSettingsController graphicsSettings;
    [SerializeField] private AudioSettingsController audioSettings;
    [SerializeField] private InputSettingsController inputSettings;
    [SerializeField] private LocalizationSettingsController localizationSettings;

    private SettingsMenuEntity menuSettings;

    public event Action OnSettingsClose;

    public void Initialize()
    {
        menuSettings = new SettingsMenuEntity();
        settingsController.Dependencies(menuSettings);
        settingsController.Initialize();

        graphicsSettings.Initialize();
        audioSettings.Initialize();
        inputSettings.Initialize();
        localizationSettings.Initialize();

        AddListeners();
    }

    public void Conclude()
    {
        settingsController.Conclude();
        graphicsSettings.Conclude();
        audioSettings.Conclude();
        inputSettings.Conclude();
        localizationSettings.Conclude();

        RemoveListeners();
    }

    private void AddListeners() => menuSettings.OnSettingsMenuClosed += () => OnSettingsClose?.Invoke();
    private void RemoveListeners() => menuSettings.OnSettingsMenuClosed -= () => OnSettingsClose?.Invoke();
    public void OpenSettingsView() => settingsController.OpenSettingsView();
}
