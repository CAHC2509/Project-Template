using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : ViewBase
{
    [SerializeField] private Button unPauseButton;
    [SerializeField] private Button settingsButton;

    private SettingsManager settingsManager;
    private GameplayEntity gameplayEntity;

    public void Dependencies(SettingsManager settingsManager, GameplayEntity gameplayEntity)
    {
        this.settingsManager = settingsManager;
        this.gameplayEntity = gameplayEntity;
        defaultSelection = unPauseButton;
    }

    public override void Initialize() => AddPersistentListeners();
    public override void Conclude() => RemovePersistentListeners();

    protected override void AddTemporaryListeners()
    {
        UIInputManager.OnCancel += gameplayEntity.UnPause;
    }

    protected override void RemoveTemporaryListeners()
    {
        UIInputManager.OnCancel -= gameplayEntity.UnPause;
    }

    protected override void AddPersistentListeners()
    {
        gameplayEntity.OnPause += EnableView;
        gameplayEntity.OnUnPause += DisableView;

        settingsManager.OnSettingsClose += EnableView;

        unPauseButton.onClick.AddListener(gameplayEntity.UnPause);
        settingsButton.onClick.AddListener(DisableView);
        settingsButton.onClick.AddListener(settingsManager.OpenSettingsView);
    }

    protected override void RemovePersistentListeners()
    {
        gameplayEntity.OnPause -= EnableView;
        gameplayEntity.OnUnPause -= DisableView;

        settingsManager.OnSettingsClose -= EnableView;
        
        unPauseButton.onClick.RemoveListener(gameplayEntity.UnPause);
        settingsButton.onClick.RemoveListener(DisableView);
        settingsButton.onClick.RemoveListener(settingsManager.OpenSettingsView);
    }
}
