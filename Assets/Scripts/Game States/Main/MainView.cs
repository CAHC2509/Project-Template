using UnityEngine;
using UnityEngine.UI;

public class MainView : ViewBase
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button requestQuitButton;

    private SettingsManager settingsManager;
    private MainEntity mainEntity;

    public void Dependencies(SettingsManager settingsManager, MainEntity mainEntity)
    {
        this.settingsManager = settingsManager;
        this.mainEntity = mainEntity;
        defaultSelection = playButton;
    }

    public override void Initialize() => AddListeners();
    public override void Conclude() => RemoveListeners();

    private void AddListeners()
    {
        playButton.onClick.AddListener(mainEntity.TriggerPlay);
        settingsButton.onClick.AddListener(mainEntity.TriggerSettings);
        requestQuitButton.onClick.AddListener(mainEntity.TriggerQuitRequest);
        
        settingsManager.OnSettingsClose += EnableView;
    }

    private void RemoveListeners()
    {
        playButton.onClick.RemoveListener(mainEntity.TriggerPlay);
        settingsButton.onClick.RemoveListener(mainEntity.TriggerSettings);
        requestQuitButton.onClick.RemoveListener(mainEntity.TriggerQuitRequest);
        
        settingsManager.OnSettingsClose -= EnableView;
    }
}
