using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip spawnSound;
    public AudioClip catchSound;
    public AudioClip missSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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