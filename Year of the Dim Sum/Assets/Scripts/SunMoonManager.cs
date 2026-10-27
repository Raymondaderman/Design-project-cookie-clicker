using System;
using UnityEngine;
using UnityEngine.UI;

public class SunMoonManager : MonoBehaviour {
	public static            SunMoonManager Instance;
	[SerializeField] private float          clickAmount;
	[SerializeField] private int            dayCycleTime;
	[SerializeField] private ParticleSystem clickParticles;

	[SerializeField] private AudioClip clickSound;

	[SerializeField] private Animator sunMoonAnimator;

	private float cycleTimer;


	public float ClickAmount {
		get => clickAmount;
		set => clickAmount = value;
	}

	private void Awake() {
		if (Instance != null) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
		}
	}

	private void Update() {
		CycleChange();
	}


	private void CycleChange() {
		if (cycleTimer > 0) {
			cycleTimer -= Time.deltaTime;
		} else {
			cycleTimer = dayCycleTime;
			sunMoonAnimator.SetTrigger("CycleSwitch");
		}
	}


	public void OnClick() {
		AudioManager.Instance.PlayASound(clickSound, 1);
		var spawnPosition = Camera.main.ScreenToWorldPoint(GameManager.Instance.MouseVector);
		Instantiate(clickParticles, spawnPosition, Quaternion.identity);
		if (GameManager.Instance.IsDay) GameManager.Instance.CoinAmount += clickAmount;
		else GameManager.Instance.CoinAmount                            += clickAmount * 2;
		Debug.Log("Moon/Sun clicked");
	}
}