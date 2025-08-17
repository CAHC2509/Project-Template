using UnityEngine;

public class MainState : GameStateBase, IMainState
{
    [SerializeField] private MainController controller;

    private SettingsMenuController settingsMenu;

    public void Dependencies(SettingsMenuController settingsMenu)
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
