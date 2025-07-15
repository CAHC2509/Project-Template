using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private SettingsMenuView view;

    private SettingsMenuEntity menuSettings;

    private void Awake() => GameManager.OnInitialization += OnInitialization;

    private void OnInitialization()
    {
        menuSettings = new SettingsMenuEntity();
        view.Initialize(menuSettings);
    }
}
