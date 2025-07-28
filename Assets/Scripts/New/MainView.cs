using System;
using UnityEngine;
using UnityEngine.UI;

public class MainView : UIViewBase
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    public event Action OnPlayPressed;
    public event Action OnSettingsPressed;
    public event Action OnQuitPressed;

    public override void Initialize()
    {
        defaultSelection = playButton;

        AddListeners();
        SetDefaultSelection();
    }

    public override void Conclude()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        playButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void RemoveListeners()
    {
        playButton.onClick.RemoveListener(StartGame);
        settingsButton.onClick.RemoveListener(OpenSettings);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    private void StartGame() => OnPlayPressed?.Invoke();
    private void OpenSettings() => OnSettingsPressed?.Invoke();
    private void QuitGame() => OnQuitPressed?.Invoke();
}
