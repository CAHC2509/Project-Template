using UnityEngine;

public class MainState : GameStateBase
{
    [SerializeField] private MainController controller;
    [SerializeField] private MainView view;

    private SettingsManager settingsManager;
    private MainEntity mainEntity;

    public void Dependencies(SettingsManager settingsManager)
    {
        this.settingsManager = settingsManager;
        settingsManager.Dependencies();

        mainEntity = new MainEntity();
        controller.Dependencies(mainEntity);
        view.Dependencies(settingsManager, mainEntity);

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        AddListeners();
        controller.Initialize();
        view.Initialize();
        settingsManager.Initialize();

        view.EnableView();
    }

    protected override void ExitState()
    {
        base.ExitState();

        RemoveListeners();
        controller.Conclude();
        view.Conclude();
        settingsManager.Conclude();
    }

    private void StartGame()
    {
        nextState = States.Results;
        ExitState();
    }

    private void AddListeners()
    {
        mainEntity.OnPlay += StartGame;
        mainEntity.OnSettings += settingsManager.OpenSettingsView;
        mainEntity.OnSettings += view.DisableView;
    }

    private void RemoveListeners()
    {
        mainEntity.OnPlay -= StartGame;
        mainEntity.OnSettings -= settingsManager.OpenSettingsView;
        mainEntity.OnSettings -= view.DisableView;
    }
}
