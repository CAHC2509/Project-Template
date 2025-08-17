using UnityEngine;
using UnityEngine.UI;

public class MainView : ViewBase
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button requestQuitButton;

    private IMainController controller;

    private void Awake()
    {
        controller = GetComponentInParent<IMainController>();
        defaultSelection = playButton;
    }

    protected override void AddPersistentListeners()
    {
        playButton.onClick.AddListener(controller.StartGame);
        settingsButton.onClick.AddListener(controller.OpenSettingsMenu);
        requestQuitButton.onClick.AddListener(controller.QuitGame);
    }

    protected override void RemovePersistentListeners()
    {
        playButton.onClick.RemoveListener(controller.StartGame);
        settingsButton.onClick.RemoveListener(controller.OpenSettingsMenu);
        requestQuitButton.onClick.RemoveListener(controller.QuitGame);
    }
}
