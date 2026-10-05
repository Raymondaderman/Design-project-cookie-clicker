using System;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "FortuneCookiePowerUpSO", menuName = "Scriptable Objects/FortuneCookiePowerUpSO")]
public class FortuneCookiePowerUpSO : ScriptableObject {
	[SerializeField]            private FactorySO affectedFactory;
	[SerializeField]            private string    powerUpName;
	[SerializeField]            private float     multiplier;
	[SerializeField]            private float     duration;
	[SerializeField]            private int       rarity;
	[SerializeField] [TextArea] private string    powerUpDescription;
	[SerializeField]            private bool      isTemporaryUpgrade;

	public string PowerUpName        => powerUpName;
	public float  Multiplier         => multiplier;
	public float  Duration           => duration;
	public int    Rarity             => rarity;
	public string PowerUpDescription => powerUpDescription;
	public bool   IsTemporaryUpgrade => isTemporaryUpgrade;
}