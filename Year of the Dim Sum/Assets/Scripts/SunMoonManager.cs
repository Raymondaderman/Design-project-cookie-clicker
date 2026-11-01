using System;
using UnityEngine;
using UnityEngine.UI;

public class SunMoonManager : MonoBehaviour {
	public static SunMoonManager Instance;

	[Header("Upgrade settings")]
	[SerializeField] private int upgradeAmount;
	[SerializeField] private int   upgradeMultiplier = 1;
	[SerializeField] private float upgradeCost;

	[SerializeField] private float          clickAmount;
	[SerializeField] private int            dayCycleTime;
	[SerializeField] private ParticleSystem clickParticles;

	[SerializeField] private AudioClip clickSound;

	[SerializeField] private Animator sunMoonAnimator;

	private float cycleTimer;

	public int UpgradeAmount {
		get => upgradeAmount;
		set => upgradeAmount = value;
	}
	public int UpgradeMultiplier {
		get => upgradeMultiplier;
		set => upgradeMultiplier = value;
	}
	public float UpgradeCost {
		get => upgradeCost;
		set => upgradeCost = value;
	}


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
		if (GameManager.Instance.IsDay) GameManager.Instance.CoinAmount += clickAmount * upgradeMultiplier;
		else GameManager.Instance.CoinAmount                            += clickAmount * upgradeMultiplier * 2;
		Debug.Log("Moon/Sun clicked");
	}
}