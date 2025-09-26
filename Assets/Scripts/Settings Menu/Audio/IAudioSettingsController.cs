using UnityEngine;

public interface IAudioSettingsController : IControllerBase, ISettingsSubmenuController
{
    public AudioSettingsEntity GetModel();
    public void UpdateGeneralVolume(float volume);
    public void UpdateMusicVolume(float volume);
    public void UpdateEffectsVolume(float volume);
    public void UpdateUIVolume(float volume);
    public string VolumeToPercentage(float volume);
}
