using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "FactorySO", menuName = "Scriptable Objects/FactorySO")]
public class FactorySO : ScriptableObject {
	[SerializeField]                           string    nameText;
	[SerializeField]                           int       cost;
	[SerializeField]                           int       costIncrement;
	[SerializeField]                           float       earnings;
	[SerializeField]                           float       cooldown;
	[SerializeField] private                   int       level;
	[SerializeField] private                   int       maxLevel;
	[SerializeField]                           Sprite    sprite;
	public bool      automatic;
	public Coroutine makeMoneyCoroutine;

	public string NameText() => nameText;
	public Sprite Sprite()   => sprite;
	public int    Cost()     => cost     + costIncrement    * level;
	public float  Cooldown() => cooldown + 0.1f - cooldown * level / maxLevel / 2;
	public int MaxLevel() => maxLevel+1;
	public int Level {
		get => level;
		set => level = value;
	}
	public float Earnings {
		get=>earnings - 1 + earnings* level / maxLevel;
		set => earnings = value;
	}
}