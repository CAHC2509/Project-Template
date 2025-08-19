using UnityEngine;

public class ResultsController : ControllerBase, IResultsController
{
    private IResultsState resultsState;
    private IViewBase view;

    private void Awake()
    {
        view = GetComponentInChildren<IViewBase>();
    }

    public void Dependencies(IResultsState resultsState)
    {
        this.resultsState = resultsState;
    }

    public override void Initialize()
    {
        base.Initialize();

        view.Initialize();
        view.EnableView();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
    }

    public void GoToMainMenu()
    {
        resultsState.GoToMainMenu();
    }

    public void PlayAgain()
    {
        resultsState.PlayAgain();
    }
}
