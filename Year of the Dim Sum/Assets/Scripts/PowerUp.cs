using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	[SerializeField] private TextMeshPro powerUpName;
	[SerializeField] private TextMeshPro powerUpDescription;

	public TextMeshPro PowerUpName {
		get => powerUpName;
		set => powerUpName = value;
	}
	public TextMeshPro PowerUpDescription {
		get => powerUpDescription;
		set => powerUpDescription = value;
	}
}