using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private SettingsMenuView view;

    private SettingsMenuEntity menuSettings;

    private void Awake()
    {
        GameManager.OnInitialization += OnInitialization;
        GameManager.OnFinalization += OnFinalization;
    }

    private void OnInitialization()
    {
        menuSettings = new SettingsMenuEntity();
        view.Initialize(menuSettings);
    }

    private void OnFinalization()
    {
        view.Conclude();
    }
}
