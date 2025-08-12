using UnityEngine;
using UnityEngine.UI;

public class GameplayView : ViewBase
{
    [Space, Header("Buttons")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resultsButton;
    [SerializeField] private Button pauseButton;

    private GameplayEntity gameplayEntity;

    public void Dependencies(GameplayEntity gameplayEntity)
    {
        this.gameplayEntity = gameplayEntity;
        defaultSelection = pauseButton;
    }

    protected override void AddTemporaryListeners()
    {
        UIInputManager.OnCancel += gameplayEntity.Pause;
    }

    protected override void RemoveTemporaryListeners()
    {
        UIInputManager.OnCancel -= gameplayEntity.Pause;
    }

    protected override void AddPersistentListeners()
    {
        gameplayEntity.OnPause += DisableView;
        gameplayEntity.OnUnPause += EnableView;

        mainMenuButton.onClick.AddListener(gameplayEntity.GoToMainMenu);
        resultsButton.onClick.AddListener(gameplayEntity.FinishMatch);
        pauseButton.onClick.AddListener(gameplayEntity.Pause);
    }

    protected override void RemovePersistentListeners()
    {
        gameplayEntity.OnPause -= DisableView;
        gameplayEntity.OnUnPause -= EnableView;

        mainMenuButton.onClick.RemoveListener(gameplayEntity.GoToMainMenu);
        resultsButton.onClick.RemoveListener(gameplayEntity.FinishMatch);
        pauseButton.onClick.RemoveListener(gameplayEntity.Pause);
    }
}
