using UnityEngine;
using UnityEngine.UI;

public class GameplayView : ViewBase, IGameplayView
{
    [Space, Header("Buttons")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resultsButton;
    [SerializeField] private Button pauseButton;

    private IGameplayController controller;

    private void Awake()
    {
        controller = GetComponentInParent<IGameplayController>();
        defaultSelection = pauseButton;
    }

    protected override void AddTemporaryListeners()
    {
        UIInputManager.OnCancel += controller.PauseGame;
    }

    protected override void RemoveTemporaryListeners()
    {
        UIInputManager.OnCancel -= controller.PauseGame;
    }

    protected override void AddPersistentListeners()
    {
        mainMenuButton.onClick.AddListener(controller.GoToMainMenu);
        resultsButton.onClick.AddListener(controller.FinishMatch);
        pauseButton.onClick.AddListener(controller.PauseGame);
    }

    protected override void RemovePersistentListeners()
    {
        mainMenuButton.onClick.RemoveListener(controller.GoToMainMenu);
        resultsButton.onClick.RemoveListener(controller.FinishMatch);
        pauseButton.onClick.RemoveListener(controller.PauseGame);
    }
}
