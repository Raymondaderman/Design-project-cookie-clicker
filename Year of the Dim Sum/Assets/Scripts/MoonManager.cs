using System;
using UnityEngine;

public class MoonManager : MonoBehaviour {
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
		GameManager.Instance.CoinAmount += moonClickAmount;
		Debug.Log("Day switched");
	}
}