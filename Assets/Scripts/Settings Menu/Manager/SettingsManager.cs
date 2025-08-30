using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour, ISettingsManager
{
    [Header("Menu")]
    [SerializeField] private SettingsMenuController menuController;
    [SerializeField] private DescriptionMessageView descriptionView;

    [Space, Header("Settings modules")]
    [SerializeField] private GraphicsSettingsController graphicsSettings;
    [SerializeField] private AudioSettingsController audioSettings;
    [SerializeField] private InputSettingsController inputSettings;
    [SerializeField] private LocalizationSettingsController localizationSettings;

    private ISettingsSubmenuController currentSubmenu;

    public event Action OnSettingsClosed;

    public void Dependencies()
    {
        menuController.Dependencies(this);
    }

    public void Initialize()
    {
        menuController.Initialize();
        descriptionView.Initialize();

        graphicsSettings.Initialize();
        audioSettings.Initialize();
        inputSettings.Initialize();
        localizationSettings.Initialize();

        AddListeners();
    }

    public void Conclude()
    {
        menuController.Conclude();
        descriptionView.Conclude();

        graphicsSettings.Conclude();
        audioSettings.Conclude();
        inputSettings.Conclude();
        localizationSettings.Conclude();

        RemoveListeners();
    }

    private void AddListeners()
    {
        menuController.OnSubmenuUpdateRequest += UpdateSubmenu;
    }

    private void RemoveListeners()
    {
        menuController.OnSubmenuUpdateRequest -= UpdateSubmenu;
    }

    private void UpdateSubmenu(SettingsSubmenuType submenuType)
    {
        if (currentSubmenu != null)
            currentSubmenu.DisableView();

        switch (submenuType)
        {
            case SettingsSubmenuType.Graphics:
                currentSubmenu = graphicsSettings;
                break;
            case SettingsSubmenuType.Audio:
                currentSubmenu = audioSettings;
                break;
            case SettingsSubmenuType.Input:
                currentSubmenu = inputSettings;
                break;
            case SettingsSubmenuType.Localization:
                currentSubmenu = localizationSettings;
                break;
            case SettingsSubmenuType.Close:
                break;
        }

        currentSubmenu.EnableView();
    }

    public void OpenSettingsView()
    {
        menuController.OpenMenuView();
    }

    public void NotifySettingsClosing()
    {
        OnSettingsClosed?.Invoke();
    }
}
