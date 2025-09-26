using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(PooledObject), typeof(AutomaticPoolReturning))]
public class FlatSFXAudioSource : MonoBehaviour, IFactoryProduct
{
    private AutomaticPoolReturning poolReturning;
    private AudioSource audioSource;

    private void Awake()
    {
        poolReturning = GetComponent<AutomaticPoolReturning>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioData audioData)
    {
        audioSource.clip = audioData.AudioClip;

        if (audioData.MinVolume != audioData.MaxVolume)
            audioSource.volume = Random.Range(audioData.MinVolume, audioData.MaxVolume);
        else
            audioSource.volume = audioData.MaxVolume;

        if (audioData.MinPitch != audioData.MaxPitch)
            audioSource.pitch = Random.Range(audioData.MinPitch, audioData.MaxPitch);
        else
            audioSource.pitch = audioData.MaxPitch;

        audioSource.Play();

        poolReturning.StartDeactivationCoroutine(audioData.AudioClip.length);
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void Conclude()
    {
        audioSource.Stop();
        poolReturning.StopDeactivationCoroutine();
    }
}
