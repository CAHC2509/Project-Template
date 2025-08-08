using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private SettingsMenuController controller;
    [SerializeField] private SettingsMenuView view;

    [Space, Header("Settings modules")]
    [SerializeField] private GraphicsSettingsController graphicsSettings;
    [SerializeField] private AudioSettingsController audioSettings;
    [SerializeField] private InputSettingsController inputSettings;
    [SerializeField] private LocalizationSettingsController localizationSettings;

    private SettingsMenuEntity menuSettings;

    public event Action OnSettingsClose;

    public void Dependencies()
    {
        menuSettings = new SettingsMenuEntity();
        controller.Dependencies(menuSettings);
        view.Dependencies(menuSettings);
    }

    public void Initialize()
    {
        controller.Initialize();
        view.Initialize();

        graphicsSettings.Initialize();
        audioSettings.Initialize();
        inputSettings.Initialize();
        localizationSettings.Initialize();

        AddListeners();
    }

    public void Conclude()
    {
        controller.Conclude();
        graphicsSettings.Conclude();
        audioSettings.Conclude();
        inputSettings.Conclude();
        localizationSettings.Conclude();

        RemoveListeners();
    }

    private void AddListeners() => menuSettings.OnSettingsMenuClosed += () => OnSettingsClose?.Invoke();
    private void RemoveListeners() => menuSettings.OnSettingsMenuClosed -= () => OnSettingsClose?.Invoke();
    public void OpenSettingsView() => view.EnableView();
}
