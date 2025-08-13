using UnityEngine;

public class GraphicsSettingsView : ViewBase, IGraphicSettingsView
{
    [Header("Resolution")]
    [SerializeField] private ResolutionView resolutionView;

    [Header("Quality")]
    [SerializeField] private QualityView qualityView;

    [Header("Full Screen")]
    [SerializeField] private FullScreenView fullScreenView;

    private IGraphicsSettingsController controller;

    private void Awake()
    {
        controller = GetComponentInParent<IGraphicsSettingsController>();
    }

    public override void Initialize()
    {
        base.Initialize();

        resolutionView.Initialize(controller);
        qualityView.Initialize(controller);
        fullScreenView.Initialize(controller);
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