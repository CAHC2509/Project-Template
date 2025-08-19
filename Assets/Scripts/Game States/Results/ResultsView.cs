using UnityEngine;
using UnityEngine.UI;

public class ResultsView : ViewBase
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    private IResultsController controller;

    private void Awake()
    {
        controller = GetComponentInParent<IResultsController>();
        defaultSelection = playAgainButton;
    }

    protected override void AddPersistentListeners()
    {
        playAgainButton.onClick.AddListener(controller.PlayAgain);
        mainMenuButton.onClick.AddListener(controller.GoToMainMenu);
    }

    protected override void RemovePersistentListeners()
    {
        playAgainButton.onClick.RemoveListener(controller.PlayAgain);
        mainMenuButton.onClick.RemoveListener(controller.GoToMainMenu);
    }
}
