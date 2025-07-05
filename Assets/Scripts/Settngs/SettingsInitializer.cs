using UnityEngine;

public class SettingsInitializer : MonoBehaviour
{
    [SerializeField] private AudioData audioData;
    [SerializeField] private GraphicsData graphicsData;

    private void Start()
    {
        new GraphicsQualityApplier(graphicsData).ApplySavedQualityLevel();
        new GraphicsResolutionApplier(graphicsData).ApplySavedResolution();
        Screen.fullScreen = FullScreenGraphicsSettings.GetFullScreen(graphicsData);

        new AudioVolumeApplier(audioData).ApplyAllVolumes();
    }
}
