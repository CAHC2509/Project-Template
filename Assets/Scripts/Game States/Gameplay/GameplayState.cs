using UnityEngine;

public class GameplayState : GameStateBase
{
    [SerializeField] private GameplayController controller;
    [SerializeField] private GameplayView view;

    private SettingsManager settingsManager;
    private GameplayEntity gameplayEntity;

    public void Dependencies(SettingsManager settingsManager)
    {
        this.settingsManager = settingsManager;
        gameplayEntity = new GameplayEntity();

        controller.Dependencies(gameplayEntity);
        view.Dependencies(settingsManager, gameplayEntity);

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        controller.Initialize();
        view.Initialize();

        view.EnableView();

        AddListeners();
    }

    protected override void ExitState()
    {
        base.ExitState();

        controller.Conclude();
        view.Conclude();

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
