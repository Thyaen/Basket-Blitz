using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioMixerGroup musicGroup;

    [Range(0f, 1f)]
    public float volume = 0.05f;

    private AudioSource audioSource;

    private static MusicManager instance;

    private void Awake()
    {
        // Prüfen, ob bereits ein MusicManager existiert
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Dieser MusicManager bleibt beim Szenenwechsel erhalten
        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = musicGroup;

        audioSource.clip = backgroundMusic;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }


    private void Update()
    {
        Debug.Log(
            $"Playing: {audioSource.isPlaying} | Enabled: {audioSource.enabled} | Clip: {audioSource.clip}"
        );
        Debug.Log(AudioListener.volume);
    }
}