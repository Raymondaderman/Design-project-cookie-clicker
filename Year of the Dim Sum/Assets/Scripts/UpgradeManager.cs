using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour {
	[SerializeField] private bool activatePowerUp;

	[SerializeField] private FortuneCookiePowerUpSO[] fortuneCookiePowerUps;

	private void Update() {
		if (!activatePowerUp) return;
		Debug.Log(RandomPowerUp());
		activatePowerUp = false;
	}

	public FortuneCookiePowerUpSO RandomPowerUp() {
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

	private IEnumerator FortuneCookiePowerUp() {
		var powerUp = RandomPowerUp();
		
		
		yield return null;
	}
}