using UnityEngine;

[DisallowMultipleComponent]
public class AudioWorks : MonoBehaviour
{
    [Header("Drag the 4 Audio Clips Here")]
    public AudioClip gameSound;
    public AudioClip jumpSound;
    public AudioClip pointSound;
    public AudioClip losingSound;

    private AudioSource musicSource;
    private AudioSource effectsSource;

    private void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        // Use the first AudioSource for background music.
        if (sources.Length >= 1)
        {
            musicSource = sources[0];
        }
        else
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        // Use the second AudioSource for sound effects.
        if (sources.Length >= 2)
        {
            effectsSource = sources[1];
        }
        else
        {
            effectsSource = gameObject.AddComponent<AudioSource>();
        }

        musicSource.playOnAwake = false;
        musicSource.loop = true;
        musicSource.volume = 1f;
        musicSource.spatialBlend = 0f;

        effectsSource.playOnAwake = false;
        effectsSource.loop = false;
        effectsSource.volume = 1f;
        effectsSource.spatialBlend = 0f;
    }

    public void PlayGameSound()
    {
        if (gameSound == null)
        {
            Debug.LogError("Game Sound is not assigned!");
            return;
        }

        // Prevent background music from starting multiple times.
        if (musicSource.isPlaying && musicSource.clip == gameSound)
        {
            return;
        }

        musicSource.Stop();
        musicSource.clip = gameSound;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayJumpSound()
    {
        PlayEffectOnce(jumpSound, "Jump Sound");
    }

    public void PlayPointSound()
    {
        PlayEffectOnce(pointSound, "Point Sound");
    }

    public void PlayLosingSound()
    {
        StopGameSound();
        PlayEffectOnce(losingSound, "Losing Sound");
    }

    public void StopGameSound()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    private void PlayEffectOnce(AudioClip clip, string soundName)
    {
        if (clip == null)
        {
            Debug.LogError(soundName + " is not assigned!");
            return;
        }

        // Stop the previous effect so sounds cannot stack or multiply.
        effectsSource.Stop();
        effectsSource.clip = clip;
        effectsSource.loop = false;
        effectsSource.Play();
    }
}