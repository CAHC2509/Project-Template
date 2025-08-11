using UnityEngine;

public class GameplayState : GameStateBase
{
    [SerializeField] private GameplayController controller;
    [SerializeField] private GameplayView view;
    [SerializeField] private PauseMenuView pauseView;

    private SettingsManager settingsManager;
    private GameplayEntity gameplayEntity;

    public void Dependencies(SettingsManager settingsManager)
    {
        this.settingsManager = settingsManager;
        gameplayEntity = new GameplayEntity();

        controller.Dependencies(gameplayEntity);
        view.Dependencies(gameplayEntity);
        pauseView.Dependencies(settingsManager, gameplayEntity);

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        controller.Initialize();
        view.Initialize();
        pauseView.Initialize();

        view.EnableView();

        AddListeners();
    }

    protected override void ExitState()
    {
        base.ExitState();

        controller.Conclude();
        view.Conclude();
        pauseView.Conclude();

        RemoveListeners();
    }

    private void AddListeners()
    {
        gameplayEntity.OnMainMenuRequest += LoadMainMenu;
        gameplayEntity.OnMatchFinished += LoadResults;
    }

    private void RemoveListeners()
    {
        gameplayEntity.OnMainMenuRequest -= LoadMainMenu;
        gameplayEntity.OnMatchFinished -= LoadResults;
    }

    private void LoadMainMenu()
    {
        nextState = States.Main;
        ExitState();
    }

    private void LoadResults()
    {
        nextState = States.Results;
        ExitState();
    }
}
