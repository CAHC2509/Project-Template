using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private FlatSFXFactory uiSFXFactory;
    [SerializeField] private FlatSFXFactory flatSFXFactory;

    private static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    public void Initialize()
    {
        uiSFXFactory.Initialize();
        flatSFXFactory.Initialize();

        backgroundMusicSource.playOnAwake = true;
        backgroundMusicSource.loop = true;
    }

    public void Conclude()
    {
        uiSFXFactory.Conclude();
        flatSFXFactory.Conclude();

        StopBackgroundMusic();
    }

    public static void SetBackgroundMusic(AudioClip music)
    {
        instance.backgroundMusicSource.Stop();
        instance.backgroundMusicSource.clip = music;
        instance.backgroundMusicSource.Play();
    }

    public static void StopBackgroundMusic()
    {
        instance.backgroundMusicSource.Stop();
    }

    public static void PlayUISFX(AudioData audioData)
    {
        IFactoryProduct product = instance.uiSFXFactory.GetProduct(Vector3.zero, Quaternion.identity);
        if (product is FlatSFXAudioSource sfx)
            sfx.Play(audioData);
    }

    public static void PlayFlatSFX(AudioData audioData)
    {
        IFactoryProduct product = instance.flatSFXFactory.GetProduct(Vector3.zero, Quaternion.identity);
        if (product is FlatSFXAudioSource sfx)
            sfx.Play(audioData);
    }
}
