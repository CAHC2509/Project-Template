using UnityEngine;
using UnityEngine.UI;

public class SaveGraphicsSettingsModalWindowView : ViewBase
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private IGraphicsSettingsController controller;

    private void Awake()
    {
        defaultSelection = confirmButton;
    }

    public void Dependencies(IGraphicsSettingsController controller)
    {
        this.controller = controller;
    }

    protected override void AddPersistentListeners()
    {
        confirmButton.onClick.AddListener(controller.SaveSettings);
        confirmButton.onClick.AddListener(CloseWindow);
        cancelButton.onClick.AddListener(CloseWindow);
    }

    protected override void RemovePersistentListeners()
    {
        confirmButton.onClick.RemoveListener(controller.SaveSettings);
        confirmButton.onClick.RemoveListener(CloseWindow);
        cancelButton.onClick.RemoveListener(CloseWindow);
    }

    private void CloseWindow()
    {
        DisableView();
        controller.EnableView();
    }
}
