using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour {
	public static            PowerUpManager Instance;
	[SerializeField] private bool           activatePowerUp;

	[SerializeField] private Canvas fortuneCookieCanvas;

	[SerializeField] private FortuneCookiePowerUpSO[] fortuneCookiePowerUps;

	private void Awake() {
		if (Instance == null) Instance = this;
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

	public IEnumerator FortuneCookiePowerUp(GameObject powerUpPopupPrefab) {
		var powerUp      = RandomPowerUp();
		var powerUpPopup = powerUpPopupPrefab.GetComponent<PowerUpPopup>();
		powerUpPopup.PowerUpName.text        = powerUp.PowerUpName;
		powerUpPopup.PowerUpDescription.text = powerUp.PowerUpDescription;

		Instantiate(powerUpPopupPrefab, powerUpPopupPrefab.transform.position, powerUpPopupPrefab.transform.rotation,
		            fortuneCookieCanvas.transform);


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