using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixerGroup sfxGroup;

    public AudioClip spawnSound;
    public AudioClip catchSound;
    public AudioClip missSound;


    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxGroup;
    }

    public void PlaySpawnSound()
    {
        audioSource.PlayOneShot(spawnSound);
    }

    public void PlayCatchSound()
    {
        audioSource.PlayOneShot(catchSound);
    }

    public void PlayMissSound()
    {
        audioSource.PlayOneShot(missSound);
    }
}