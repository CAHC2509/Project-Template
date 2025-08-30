using UnityEngine;

public class MainState : GameStateBase, IMainState
{
    [SerializeField] private MainController controller;

    private SettingsManager settingsMenu;

    public void Dependencies(SettingsManager settingsMenu)
    {
        this.settingsMenu = settingsMenu;

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        controller.Dependencies(this, settingsMenu);
        controller.Initialize();
    }

    protected override void ExitState()
    {
        base.ExitState();

        controller.Conclude();
    }

    public void StartGame()
    {
        nextState = States.Gameplay;
        ExitState();
    }
}
