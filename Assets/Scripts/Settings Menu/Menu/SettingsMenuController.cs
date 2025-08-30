using System;
using UnityEngine;

public class SettingsMenuController : ControllerBase, ISettingsMenuController
{
    public event Action<SettingsSubmenuType> OnSubmenuUpdateRequest;
    private ISettingsMenuView view;
    SettingsManager settingsManager;

    private void Awake()
    {
        view = GetComponentInChildren<ISettingsMenuView>();
    }

    public void Dependencies(SettingsManager settingsManager)
    {
        this.settingsManager = settingsManager;
    }

    public override void Initialize()
    {
        base.Initialize();

        view.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
    }

    public void OpenMenuView()
    {
        view.EnableView();
    }

    public void CloseMenuView()
    {
        view.DisableView();
        settingsManager.NotifySettingsClosing();
    }

    public void ChangeCurrentPanel(SettingsSubmenuType submenuType)
    {
        OnSubmenuUpdateRequest?.Invoke(submenuType);
    }
}