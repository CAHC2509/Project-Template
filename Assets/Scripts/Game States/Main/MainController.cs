using UnityEngine;

public class MainController : ControllerBase, IMainController
{
    SettingsManager settingsManager;
    private IMainState mainState;
    private IViewBase view;

    private void Awake()
    {
        view = GetComponentInChildren<IViewBase>();
    }

    public void Dependencies(IMainState mainState, SettingsManager settingsManager)
    {
        this.mainState = mainState;
        this.settingsManager = settingsManager;
    }

    public override void Initialize()
    {
        base.Initialize();

        view.Initialize();
        view.EnableView();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
    }

    protected override void AddListeners()
    {
        settingsManager.OnSettingsClosed += view.EnableView;
    }

    protected override void RemoveListeners()
    {
        settingsManager.OnSettingsClosed -= view.EnableView;
    }

    public void StartGame()
    {
        mainState.StartGame();
    }

    public void StartTutorial()
    {
        mainState.StartTutorial();
    }

    public void OpenSettingsMenu()
    {
        view.DisableView();
        settingsManager.OpenSettingsView();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
