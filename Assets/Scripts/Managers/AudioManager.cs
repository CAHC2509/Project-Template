using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    public void PlaySFX(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            Debug.LogWarning("Triying to play a null SFX");
            return;
        }

        sfxAudioSource.PlayOneShot(audioClip);
    }
}
