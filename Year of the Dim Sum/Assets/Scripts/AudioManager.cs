using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource source;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayASound(AudioClip sound, float vol)
    {
        source.PlayOneShot(sound, vol);
    }
}
