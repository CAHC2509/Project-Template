using System;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private SettingsMenuView view;
    
    private SettingsMenuEntity menuSettings;
    
    public void Dependencies(SettingsMenuEntity menuSettings) => this.menuSettings = menuSettings;

    public void Initialize()
    {
        view.Dependencies(menuSettings);
        view.Initialize();
        AddListeners();
    }

    public void Conclude()
    {
        view.Conclude();
        RemoveListeners();
    }

    private void AddListeners()
    {
        menuSettings.OnSettingsMenuClosed += CloseSettingsView;
    }

    private void RemoveListeners()
    {
        menuSettings.OnSettingsMenuClosed -= CloseSettingsView;
    }

    public void OpenSettingsView() => view.EnableView();
    public void CloseSettingsView() => view.DisableView();
}
