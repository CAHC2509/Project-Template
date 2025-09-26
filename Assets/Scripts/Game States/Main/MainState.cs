using UnityEngine;

public class MainState : GameStateBase, IMainState
{
    [SerializeField] private MainController controller;
    [SerializeField] private AudioClip mainMenuMusic;

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

        AudioManager.SetBackgroundMusic(mainMenuMusic);
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

    public void StartTutorial()
    {
        nextState = States.Onboarding;
        ExitState();
    }
}
