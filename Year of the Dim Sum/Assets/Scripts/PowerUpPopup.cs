using TMPro;
using UnityEngine;

public class PowerUpPopup : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI powerUpName;
	[SerializeField] private TextMeshProUGUI powerUpDescription;

	public TextMeshProUGUI PowerUpName {
		get => powerUpName;
		set => powerUpName = value;
	}
	public TextMeshProUGUI PowerUpDescription {
		get => powerUpDescription;
		set => powerUpDescription = value;
	}
}