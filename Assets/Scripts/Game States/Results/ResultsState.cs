using UnityEngine;

public class ResultsState : GameStateBase, IResultsState
{
    [SerializeField] private ResultsController controller;

    public void Dependencies()
    {
        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        controller.Dependencies(this);
        controller.Initialize();
    }

    protected override void ExitState()
    {
        base.ExitState();

        controller.Conclude();
    }

    public void GoToMainMenu()
    {
        nextState = States.Main;
        ExitState();
    }

    public void PlayAgain()
    {
        nextState = States.Gameplay;
        ExitState();
    }
}
