using UnityEngine;

public class GraphicsSettingsView : ViewBase
{
    [Header("Resolution")]
    [SerializeField] private ResolutionView resolutionView;

    [Header("Quality")]
    [SerializeField] private QualityView qualityView;

    [Header("Full Screen")]
    [SerializeField] private FullScreenView fullScreenView;

    private GraphicsSettingsEntity graphicsSettings;

    public void Dependencies(GraphicsSettingsEntity graphicsSettings)
    {
        this.graphicsSettings = graphicsSettings;

        resolutionView.Dependencies(graphicsSettings);
        qualityView.Dependencies(graphicsSettings);
        fullScreenView.Dependencies(graphicsSettings);
    }

    public override void Initialize()
    {
        resolutionView.Initialize();
        qualityView.Initialize();
        fullScreenView.Initialize();
    }

    public override void Conclude()
    {
        resolutionView.Conclude();
        qualityView.Conclude();
        fullScreenView.Conclude();
    }
}