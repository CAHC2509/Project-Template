using UnityEngine;

public class MainController : ControllerBase, IMainController
{
    ISettingsMenuController settingsMenu;
    private IMainState mainState;
    private IViewBase view;

    private void Awake()
    {
        view = GetComponentInChildren<IViewBase>();
    }

    public void Dependencies(IMainState mainState, ISettingsMenuController settingsMenu)
    {
        this.mainState = mainState;
        this.settingsMenu = settingsMenu;
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
        settingsMenu.OnSettingsClosed += view.EnableView;
    }

    protected override void RemoveListeners()
    {
        settingsMenu.OnSettingsClosed -= view.EnableView;
    }

    public void StartGame()
    {
        mainState.StartGame();
    }

    public void OpenSettingsMenu()
    {
        view.DisableView();
        settingsMenu.OpenSettingsView();
    }

    public void QuitGame() => Application.Quit();
}
