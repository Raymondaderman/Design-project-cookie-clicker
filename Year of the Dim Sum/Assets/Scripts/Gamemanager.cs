using UnityEngine;

public class GameManager : MonoBehaviour {
	public static                   GameManager Instance;
	[field: SerializeField] private float       coinAmount;

	private void Awake() {
		if (Instance != null) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
		}
	}

	public float CoinAmount {
		get => coinAmount;
		set => coinAmount = value;
	}
}