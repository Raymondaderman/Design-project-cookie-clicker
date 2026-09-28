using System;
using UnityEngine;

public class SunMoonManager : MonoBehaviour {
	[SerializeField] private float moonClickAmount;
	[SerializeField] private int   dayCycleTime;
	private                  float cycleTimer;


	[SerializeField] private Animator sunMoonAnimator;

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
		if (GameManager.Instance.IsDay) GameManager.Instance.CoinAmount += moonClickAmount;
		else GameManager.Instance.CoinAmount                            += moonClickAmount * 2;
		Debug.Log("Moon/Sun clicked");
	}
}