using UnityEngine;

public class CookingAudio : MonoBehaviour
{
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBubblesSFX()
    {
        if(!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopSFX()
    {
        if(audioSource.isPlaying)
            audioSource.Stop();
    }
}