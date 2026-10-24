using System;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour {
	[Header("General Upgrade settings")]
	[SerializeField] private float upgradeCostMultiplier;
	[SerializeField] private float     maxUpgradeAmount;
	[SerializeField] private FactorySO affectedFactory;

	[Header("Button Visuals")]
	[SerializeField] private TextMeshProUGUI upgradeName;
	[SerializeField] private TextMeshProUGUI upgradeMultiplier;
	[SerializeField] private TextMeshProUGUI upgradeCost;


	private void Awake() {
		upgradeName.text       = affectedFactory.name;
		upgradeMultiplier.text = affectedFactory.UpgradeMultiplier + "X";
		upgradeCost.text       = affectedFactory.UpgradeCost       + " Coins";
	}


	public void PurchaseUpgrade() {
		if (affectedFactory.UpgradeAmount >= maxUpgradeAmount) return;
		if (affectedFactory.UpgradeCost   > GameManager.Instance.CoinAmount) return;

		GameManager.Instance.CoinAmount -= affectedFactory.UpgradeCost;


		affectedFactory.UpgradeCost *= upgradeCostMultiplier;
		affectedFactory.UpgradeAmount++;
		affectedFactory.UpgradeMultiplier++;

		upgradeName.text       = affectedFactory.name;
		upgradeMultiplier.text = affectedFactory.UpgradeMultiplier + "X";
		upgradeCost.text       = affectedFactory.UpgradeCost       + " Coins";
	}
}