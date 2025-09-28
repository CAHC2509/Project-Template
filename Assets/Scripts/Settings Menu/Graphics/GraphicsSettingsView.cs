using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettingsView : ViewBase, IGraphicSettingsView
{
    [Header("Resolution")]
    [SerializeField] private Button resolutionButton;
    [SerializeField] private ResolutionView resolutionView;

    [Header("Quality")]
    [SerializeField] private QualityView qualityView;

    [Header("Full Screen")]
    [SerializeField] private FullScreenView fullScreenView;

    [Header("Save and default")]
    [SerializeField] private Button defaultButton;
    [SerializeField] private Button saveButton;

    private IGraphicsSettingsController controller;

    private void Awake()
    {
        controller = GetComponentInParent<IGraphicsSettingsController>();
        defaultSelection = resolutionButton;
    }

    public override void Initialize()
    {
        base.Initialize();

        resolutionView.Initialize(controller);
        qualityView.Initialize(controller);
        fullScreenView.Initialize(controller);
    }

    protected override void AddPersistentListeners()
    {
        defaultButton.onClick.AddListener(controller.SetDefaultSettings);
        defaultButton.onClick.AddListener(controller.SaveSettings);
        saveButton.onClick.AddListener(controller.SaveSettings);
    }

    protected override void RemovePersistentListeners()
    {
        defaultButton.onClick.RemoveListener(controller.SetDefaultSettings);
        defaultButton.onClick.RemoveListener(controller.SaveSettings);
        saveButton.onClick.RemoveListener(controller.SaveSettings);
    }

    public override void Conclude()
    {
        base.Conclude();

        resolutionView.Conclude();
        qualityView.Conclude();
        fullScreenView.Conclude();
    }

    public void UpdateResolutionView()
    {
        resolutionView.UpdateResolutionText();
        resolutionView.UpdateResolutionButtons();
    }

    public void UpdateQualityLevelView()
    {
        qualityView.UpdateQualityLevelText();
        qualityView.UpdateQualityLevelButtons();
    }

    public void UpdateFullScreenModeView()
    {
        fullScreenView.UpdateFullScreenModeText();
        fullScreenView.UpdateFullScreenModeButtons();
    }
}