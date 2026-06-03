using UnityEngine;

public class AudioSoundManager : MonoBehaviour
{
    public static AudioSoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] AudioSource hurtAudioSource;
    [SerializeField] AudioSource collectAudioSource;
    [SerializeField] AudioSource loseAudioSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip hurtClip;
    [SerializeField] AudioClip collectClip;
    [SerializeField] AudioClip loseClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayHurtSound()
    {
        hurtAudioSource.PlayOneShot(hurtClip);
    }

    public void PlayCollectSound()
    {
        collectAudioSource.PlayOneShot(collectClip);
    }

    public void PlayLoseSound()
    {
        loseAudioSource.PlayOneShot(loseClip);
    }
}
