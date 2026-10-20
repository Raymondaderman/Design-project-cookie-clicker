using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "FactorySO", menuName = "Scriptable Objects/FactorySO")]
public class FactorySO : ScriptableObject {
	[SerializeField]         string nameText;
	[SerializeField]         int    cost;
	[SerializeField]         int    costIncrement;
	[SerializeField]         float  earnings;
	[SerializeField]         float  cooldown;
	[SerializeField] private int    level;
	[SerializeField] private int    maxLevel;

	[Header("Upgrade settings")]
	[SerializeField] private int upgradeAmount;
	[SerializeField] private int   upgradeMultiplier = 1;
	[SerializeField] private float upgradeCost;

	[SerializeField] Sprite    sprite;
	public           bool      automatic;
	public           Coroutine makeMoneyCoroutine;
	public           int       Cost()     => cost            + costIncrement    * level;
	public           float     Cooldown() => cooldown + 0.1f - cooldown * level / maxLevel / 2;
	public           int       MaxLevel() => maxLevel        + 1;
	public int Level {
		get => level;
		set => level = value;
	}
	public float Earnings {
		get => earnings - 1 + earnings * level / maxLevel * upgradeMultiplier;
		set => earnings = value;
	}
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
}