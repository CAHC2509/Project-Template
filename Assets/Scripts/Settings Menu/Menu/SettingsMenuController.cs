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
        menuSettings.OnSettingsMenuClosed += () => view.DisableView();
    }

    private void RemoveListeners()
    {
        menuSettings.OnSettingsMenuClosed -= () => view.DisableView();
    }
}
