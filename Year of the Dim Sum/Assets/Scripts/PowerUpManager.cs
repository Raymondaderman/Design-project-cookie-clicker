using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour {
	[SerializeField] private bool activatePowerUp;

	[SerializeField] private FortuneCookiePowerUpSO[] fortuneCookiePowerUps;

	private void Update() {
		if (!activatePowerUp) return;
		StartCoroutine(FortuneCookiePowerUp());
		activatePowerUp = false;
	}

	private FortuneCookiePowerUpSO RandomPowerUp() {
		Debug.Log("Entered PowerUp generator");
		var totalTickets = 0;
		foreach (var powerUp in fortuneCookiePowerUps) {
			totalTickets += powerUp.Rarity;
		}

		var chosenTicket = Random.Range(0, totalTickets);
		foreach (var powerUp in fortuneCookiePowerUps) {
			chosenTicket -= powerUp.Rarity;
			if (chosenTicket <= 0) return powerUp;
		}

		return null;
	}

	public IEnumerator FortuneCookiePowerUp() {
		Debug.Log("Entered PowerUp IEnumerator");
		var powerUp = RandomPowerUp();

		if (powerUp.IsClickPowerUp) {
			SunMoonManager.Instance.ClickAmount *= powerUp.Multiplier;
			yield return new WaitForSeconds(powerUp.Duration);
			SunMoonManager.Instance.ClickAmount /= powerUp.Multiplier;
		} else {
			var factorySo = powerUp.AffectedFactory;
			factorySo.Earnings *= powerUp.Multiplier;
			yield return new WaitForSeconds(powerUp.Duration);
			factorySo.Earnings /= powerUp.Multiplier;
		}

		yield return null;
	}
}