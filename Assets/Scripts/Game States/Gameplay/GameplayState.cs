using UnityEngine;

public class GameplayState : GameStateBase, IGameplayState
{
    [SerializeField] private GameplayController controller;

    private SettingsMenuController settingsManager;

    public void Dependencies(SettingsMenuController settingsManager)
    {
        this.settingsManager = settingsManager;
        controller.Dependencies(this, settingsManager);

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        controller.Initialize();
    }

    protected override void ExitState()
    {
        base.ExitState();

        controller.Conclude();
    }

    public void LoadMainMenu()
    {
        nextState = States.Main;
        ExitState();
    }

    public void LoadResults()
    {
        nextState = States.Results;
        ExitState();
    }
}
