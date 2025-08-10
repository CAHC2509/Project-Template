using UnityEngine;
using UnityEngine.UI;

public class GameplayView : ViewBase
{
    [Space, Header("Buttons")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resultsButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button unPauseButton;
    [SerializeField] private Button settingsButton;

    [Space, Header("Panels")]
    [SerializeField] private GameObject pausePanel;

    private SettingsManager settingsManager;
    private GameplayEntity gameplayEntity;

    public void Dependencies(SettingsManager settingsManager, GameplayEntity gameplayEntity)
    {
        this.settingsManager = settingsManager;
        this.gameplayEntity = gameplayEntity;
        defaultSelection = pauseButton;
    }

    public override void Initialize()
    {
        AddListeners();
        HidePausePanel();
    }

    public override void Conclude()
    {
        RemoveListeners();
        HidePausePanel();
    }

    private void AddListeners()
    {
        settingsManager.OnSettingsClose += ShowPausePanel;

        gameplayEntity.OnPause += ShowPausePanel;
        gameplayEntity.OnPause += DisableView;
        gameplayEntity.OnUnPause += HidePausePanel;
        gameplayEntity.OnUnPause += EnableView;

        mainMenuButton.onClick.AddListener(gameplayEntity.GoToMainMenu);
        resultsButton.onClick.AddListener(gameplayEntity.FinishMatch);
        pauseButton.onClick.AddListener(gameplayEntity.Pause);
        unPauseButton.onClick.AddListener(gameplayEntity.UnPause);
        settingsButton.onClick.AddListener(HidePausePanel);
        settingsButton.onClick.AddListener(settingsManager.OpenSettingsView);
    }

    private void RemoveListeners()
    {
        settingsManager.OnSettingsClose -= ShowPausePanel;

        gameplayEntity.OnPause -= ShowPausePanel;
        gameplayEntity.OnPause -= DisableView;
        gameplayEntity.OnUnPause -= HidePausePanel;
        gameplayEntity.OnUnPause -= EnableView;

        mainMenuButton.onClick.RemoveListener(gameplayEntity.GoToMainMenu);
        resultsButton.onClick.RemoveListener(gameplayEntity.FinishMatch);
        pauseButton.onClick.RemoveListener(gameplayEntity.Pause);
        unPauseButton.onClick.RemoveListener(gameplayEntity.UnPause);
        settingsButton.onClick.RemoveListener(HidePausePanel);
        settingsButton.onClick.RemoveListener(settingsManager.OpenSettingsView);
    }

    private void ShowPausePanel() => pausePanel.SetActive(true);
    private void HidePausePanel() => pausePanel.SetActive(false);
}
