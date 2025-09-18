using UnityEngine;

public class OnboardingState : GameStateBase, IOnboardingState
{
    [SerializeField] private OnboardingController controller;

    private SettingsManager settingsManager;

    public void Dependencies(SettingsManager settingsManager)
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

    public void PlayAgain()
    {
        nextState = States.Onboarding;
        ExitState();
    }

    public void StartGameplay()
    {
        nextState = States.Gameplay;
        ExitState();
    }

    public void GoToMainMenu()
    {
        nextState = States.Main;
        ExitState();
    }
}
