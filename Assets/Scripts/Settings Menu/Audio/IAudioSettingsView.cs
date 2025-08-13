using UnityEngine;

public interface IAudioSettingsView : IViewBase
{
    public void UpdateGeneralVolumeText();
    public void UpdateMusicVolumeText();
    public void UpdateEffectsVolumeText();
}
