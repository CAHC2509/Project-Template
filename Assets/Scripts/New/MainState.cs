using UnityEngine;

public class MainState : GameStateBase
{
    [SerializeField] private MainController main;

    private SettingsManager settingsManager;

    public void Dependencies(SettingsManager settingsManager)
    {
        this.settingsManager = settingsManager;
        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        AddListeners();
        OpenMainMenu();
        settingsManager.Initialize();
    }

    protected override void ExitState()
    {
        base.ExitState();

        RemoveListeners();
        settingsManager.Conclude();
    }

    private void AddListeners()
    {
        main.OnPlay += OnPlay;
        main.OnSettings += OnSettings;
        settingsManager.OnSettingsClose += OpenMainMenu;
    }

    private void RemoveListeners()
    {
        main.OnPlay -= OnPlay;
        main.OnSettings -= OnSettings;
        settingsManager.OnSettingsClose -= OpenMainMenu;
    }

    private void OpenMainMenu()
    {
        main.Initialize();
        main.OpenMainView();
    }

    private void CloseMainMenu()
    {
        main.Conclude();
        main.CloseMainView();
    }

    private void OnPlay()
    {
        nextState = States.Results;
        ExitState();
    }

    private void OnSettings()
    {
        CloseMainMenu();
        settingsManager.OpenSettingsView();
    }
}
