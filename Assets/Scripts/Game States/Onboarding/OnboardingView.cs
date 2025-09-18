using UnityEngine;
using UnityEngine.UI;

public class OnboardingView : ViewBase
{
    [SerializeField] private Button startGameplayButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button pauseButton;

    private IOnboardingController controller;

    private void Awake()
    {
        controller = GetComponentInParent<IOnboardingController>();
        defaultSelection = pauseButton;
    }

    protected override void AddTemporaryListeners() => UIInputManager.OnCancel += controller.PauseGame;
    protected override void RemoveTemporaryListeners() => UIInputManager.OnCancel -= controller.PauseGame;

    protected override void AddPersistentListeners()
    {
        playAgainButton.onClick.AddListener(controller.PlayAgain);
        startGameplayButton.onClick.AddListener(controller.FinishMatch);
        mainMenuButton.onClick.AddListener(controller.GoToMainMenu);
        pauseButton.onClick.AddListener(controller.PauseGame);
    }

    protected override void RemovePersistentListeners()
    {
        playAgainButton.onClick.RemoveListener(controller.PlayAgain);
        startGameplayButton.onClick.RemoveListener(controller.FinishMatch);
        mainMenuButton.onClick.RemoveListener(controller.GoToMainMenu);
        pauseButton.onClick.RemoveListener(controller.PauseGame);
    }
}
