using UnityEngine;

public class ChestAudio : MonoBehaviour
{
    [SerializeField] AudioClip openSFX;
    [SerializeField] AudioClip closeSFX;
    AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpenSFX()
    {
        audioSource.clip = openSFX;
        audioSource.Play();
    }

    public void PlayCloseSFX()
    {
        audioSource.clip = closeSFX;
        audioSource.Play();
    }
}