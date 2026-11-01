using System;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour {
	[Header("General Upgrade settings")]
	[SerializeField] private float upgradeCostMultiplier;
	[SerializeField] private float     maxUpgradeAmount;
	[SerializeField] private FactorySO affectedFactory;
	[SerializeField] private AudioClip buttonSound;
	[SerializeField] private bool      isSunMoonUpgrade;

	[Header("Button Visuals")]
	[SerializeField] private TextMeshProUGUI upgradeName;
	[SerializeField] private TextMeshProUGUI upgradeMultiplier;
	[SerializeField] private TextMeshProUGUI upgradeCost;


	private void Awake() {
		if (isSunMoonUpgrade) {
			upgradeName.text       = "Sun/Moon";
			upgradeMultiplier.text = SunMoonManager.Instance.UpgradeMultiplier + "X";
			upgradeCost.text       = "Cost:"                                   + SunMoonManager.Instance.UpgradeCost;
		} else {
			upgradeName.text       = affectedFactory.name;
			upgradeMultiplier.text = affectedFactory.UpgradeMultiplier + "X";
			upgradeCost.text       = "Cost:"                           + affectedFactory.UpgradeCost;
		}
	}


	public void PurchaseUpgrade() {
		AudioManager.Instance.PlayASound(buttonSound, 1f);
		if (isSunMoonUpgrade) {
			if (SunMoonManager.Instance.UpgradeAmount >= maxUpgradeAmount) return;
			if (SunMoonManager.Instance.UpgradeCost   > GameManager.Instance.CoinAmount) return;

			GameManager.Instance.CoinAmount -= SunMoonManager.Instance.UpgradeCost;


			SunMoonManager.Instance.UpgradeCost *= upgradeCostMultiplier;
			SunMoonManager.Instance.UpgradeAmount++;
			SunMoonManager.Instance.UpgradeMultiplier++;

			upgradeName.text       = "Sun/Moon";
			upgradeMultiplier.text = SunMoonManager.Instance.UpgradeMultiplier + "X";
			upgradeCost.text       = "Cost:"                                   + SunMoonManager.Instance.UpgradeCost;
		} else {
			if (affectedFactory.UpgradeAmount >= maxUpgradeAmount) return;
			if (affectedFactory.UpgradeCost   > GameManager.Instance.CoinAmount) return;

			GameManager.Instance.CoinAmount -= affectedFactory.UpgradeCost;


			affectedFactory.UpgradeCost *= upgradeCostMultiplier;
			affectedFactory.UpgradeAmount++;
			affectedFactory.UpgradeMultiplier++;

			upgradeName.text       = affectedFactory.name;
			upgradeMultiplier.text = affectedFactory.UpgradeMultiplier + "X";
			upgradeCost.text       = "Cost:"                           + affectedFactory.UpgradeCost;
		}
	}
}