using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FactorySO", menuName = "Scriptable Objects/FactorySO")]
public class FactorySO : ScriptableObject {
	#region Private variables
	[Header("Base stats")]
	[SerializeField] private string name;
	[SerializeField] private int    cost;
	[SerializeField] private int    costIncrement;
	[SerializeField] private float  earnings;
	[SerializeField] private float  cooldown;
	[SerializeField] private int    level;
	[SerializeField] private int    maxLevel;
	[SerializeField] private Sprite sprite;

	[Header("Upgrade settings")]
	[SerializeField] private int upgradeAmount;
	[SerializeField] private int   upgradeMultiplier = 1;
	[SerializeField] private float upgradeCost;
	#endregion
	#region Public variables
	public bool      automatic;
	public Coroutine makeMoneyCoroutine;
	public string    Name()     => name;
	public int       Cost()     => cost            + costIncrement    * level;
	public float     Cooldown() => cooldown + 0.1f - cooldown * level / maxLevel / 2;
	public Sprite    Sprite()   => sprite;
	public int       MaxLevel() => maxLevel        + 1;
	#endregion
	#region Getters and setters
	public int Level {
		get => level;
		set => level = value;
	}
	public float Earnings {
		get => earnings - 1;
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
	#endregion
}

public class sprite {
}