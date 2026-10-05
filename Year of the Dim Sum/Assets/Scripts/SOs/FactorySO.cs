using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "FactorySO", menuName = "Scriptable Objects/FactorySO")]
public class FactorySO : ScriptableObject {
	[SerializeField]                           string    nameText;
	[SerializeField]                           int       cost;
	[SerializeField]                           int       costIncrement;
	[SerializeField]                           int       earnings;
	[SerializeField]                           int       cooldown;
	[SerializeField] private                   int       level;
	[SerializeField] private                   int       maxLevel;
	[SerializeField]                           Sprite    sprite;
	public bool      automatic;
	public Coroutine makeMoneyCoroutine;

	public string NameText() => nameText;
	public Sprite Sprite()   => sprite;
	public int    Cost()     => cost     + costIncrement    * level;
	public int    Earnings() => earnings + earnings * level / maxLevel;
	public int    Cooldown() => cooldown - cooldown * level / maxLevel / 2;
	public int Level {
		get => level;
		set => level = value;
	}
}