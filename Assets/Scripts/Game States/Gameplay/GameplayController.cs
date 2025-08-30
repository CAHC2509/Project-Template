using UnityEngine;

public class GameplayController : ControllerBase, IGameplayController
{
    SettingsManager settingsManager;
    private IGameplayState gameplayState;
    private IGameplayView gameplayView;
    private IPauseMenuView pauseView;
    private float lastTimeScale = 1f;

    private void Awake()
    {
        gameplayView = GetComponentInChildren<IGameplayView>();
        pauseView = GetComponentInChildren<IPauseMenuView>();
    }

    public void Dependencies(IGameplayState gameplayState, SettingsManager settingsManager)
    {
        this.gameplayState = gameplayState;
        this.settingsManager = settingsManager;
    }

    public override void Initialize()
    {
        base.Initialize();

        gameplayView.Initialize();
        pauseView.Initialize();
        UnPauseGame();
    }

    public override void Conclude()
    {
        base.Conclude();
        
        gameplayView.Conclude();
        pauseView.Conclude();
        UnPauseGame();
    }

    protected override void AddListeners()
    {
        settingsManager.OnSettingsClosed += pauseView.EnableView;
    }

    protected override void RemoveListeners()
    {
        settingsManager.OnSettingsClosed -= pauseView.EnableView;
    }

    public void PauseGame()
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        gameplayView.DisableView();
        pauseView.EnableView();
    }

    public void UnPauseGame()
    {
        Time.timeScale = lastTimeScale;

        pauseView.DisableView();
        gameplayView.EnableView();
    }

    public void FinishMatch()
    {
        gameplayState.LoadResults();
    }

    public void GoToMainMenu()
    {
        gameplayState.LoadMainMenu();
    }

    public void OpenSettingsMenu()
    {
        pauseView.DisableView();
        settingsManager.OpenSettingsView();
    }
}
