using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings/Audio Data", fileName = "Audio Data")]
public class AudioData : ScriptableObject
{
    [field: SerializeField] public AudioMixer MainMixer { get; private set; }
    [field: SerializeField] public string MainVolumeKey { get; private set; }
    [field: SerializeField] public string MusicVolumeKey { get; private set; }
    [field: SerializeField] public string SFXVolumeKey { get; private set; }
}