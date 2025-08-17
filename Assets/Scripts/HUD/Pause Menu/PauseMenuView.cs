using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : ViewBase, IPauseMenuView
{
    [SerializeField] private Button unPauseButton;
    [SerializeField] private Button settingsButton;

    private IGameplayController controller;

    private void Awake()
    {
        controller = GetComponentInParent<IGameplayController>();
        defaultSelection = unPauseButton;
    }

    protected override void AddTemporaryListeners() => UIInputManager.OnCancel += controller.UnPauseGame;
    protected override void RemoveTemporaryListeners() => UIInputManager.OnCancel -= controller.UnPauseGame;

    protected override void AddPersistentListeners()
    {
        unPauseButton.onClick.AddListener(controller.UnPauseGame);
        settingsButton.onClick.AddListener(controller.OpenSettingsMenu);
    }

    protected override void RemovePersistentListeners()
    {
        unPauseButton.onClick.RemoveListener(controller.UnPauseGame);
        settingsButton.onClick.RemoveListener(controller.OpenSettingsMenu);
    }
}
