using UnityEngine;

public class OnboardingController : ControllerBase, IOnboardingController
{
    private SettingsManager settingsManager;
    private IOnboardingState onboardingState;
    private IViewBase onboardingView;
    private IPauseMenuView pauseView;
    private float lastTimeScale = 1f;

    private void Awake()
    {
        onboardingView = GetComponentInChildren<IViewBase>();
        pauseView = GetComponentInChildren<IPauseMenuView>();
    }

    public void Dependencies(IOnboardingState onboardingState, SettingsManager settingsManager)
    {
        this.onboardingState = onboardingState;
        this.settingsManager = settingsManager;
    }

    public override void Initialize()
    {
        base.Initialize();

        onboardingView.Initialize();
        pauseView.Initialize();
        UnPauseGame();
    }

    public override void Conclude()
    {
        base.Conclude();

        onboardingView.Conclude();
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

        onboardingView.DisableView();
        pauseView.EnableView();
    }

    public void UnPauseGame()
    {
        Time.timeScale = lastTimeScale;

        pauseView.DisableView();
        onboardingView.EnableView();
    }

    public void PlayAgain()
    {
        onboardingState.PlayAgain();
    }

    public void GoToMainMenu()
    {
        onboardingState.GoToMainMenu();
    }

    public void FinishMatch()
    {
        onboardingState.StartGameplay();
    }

    public void OpenSettingsMenu()
    {
        pauseView.DisableView();
        settingsManager.OpenSettingsView();
    }
}
