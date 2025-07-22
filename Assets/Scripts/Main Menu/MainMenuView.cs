using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private SelectableStateController defaultSelection;

    public static event Action OnPlayPressed;
    public static event Action OnSettingsPressed;
    public static event Action OnQuitPressed;

    private void Awake()
    {
        GameManager.OnInitialization += OnInitialization;
        GameManager.OnFinalization += OnFinalization;
    }

    private void OnInitialization()
    {
        AddListeners();
        OpenMainMenu();
    }

    private void OnFinalization()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        playButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);

        SettingsMenuView.OnSettingsMenuClose += OpenMainMenu;
    }

    private void RemoveListeners()
    {
        playButton.onClick.RemoveListener(StartGame);
        settingsButton.onClick.RemoveListener(OpenSettings);
        quitButton.onClick.RemoveListener(QuitGame);

        SettingsMenuView.OnSettingsMenuClose -= OpenMainMenu;
    }

    private void OpenSettings()
    {
        mainMenu.SetActive(false);
        OnSettingsPressed?.Invoke();
    }

    private void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        SelectionManager.Instance.Select(defaultSelection);
    }

    private void StartGame() => OnPlayPressed?.Invoke();
    private void QuitGame() => OnQuitPressed?.Invoke();
}
