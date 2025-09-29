using UnityEngine;

public interface IAudioSettingsView : IViewBase
{
    public void UpdateGeneralVolumeText();
    public void UpdateMusicVolumeText();
    public void UpdateEffectsVolumeText();
    public void UpdateUIVolumeText();
    public void SetSliders();
    public void SetTexts();
}
