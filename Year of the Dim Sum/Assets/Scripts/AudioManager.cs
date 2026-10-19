using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static            AudioManager Instance;
	[SerializeField] private AudioSource  musicAudioSource;

	AudioSource source;

	private void Awake() {
		if (Instance == null) Instance = this;
	}

	private void Start() {
		source = GetComponent<AudioSource>();
		PlayBGMusic();
	}

	public void PlayASound(AudioClip sound, float vol) {
		source.PlayOneShot(sound, vol);
	}

	private void PlayBGMusic() {
		musicAudioSource.Play();
	}
}