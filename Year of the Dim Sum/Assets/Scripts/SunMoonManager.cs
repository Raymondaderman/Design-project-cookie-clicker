using System;
using UnityEngine;

public class SunMoonManager : MonoBehaviour {
	public static            SunMoonManager Instance;
	[SerializeField] private float          clickAmount;
	[SerializeField] private int            dayCycleTime;
	private                  float          cycleTimer;
	
	[SerializeField] private Animator sunMoonAnimator;
	
	
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
		if (GameManager.Instance.IsDay) GameManager.Instance.CoinAmount += clickAmount;
		else GameManager.Instance.CoinAmount                            += clickAmount * 2;
		Debug.Log("Moon/Sun clicked");
	}
}