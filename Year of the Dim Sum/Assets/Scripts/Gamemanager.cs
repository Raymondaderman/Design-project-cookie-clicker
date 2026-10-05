using UnityEngine;

public class GameManager : MonoBehaviour {
	public static                   GameManager Instance;
	[Header("General Information")]
	[field: SerializeField] private float       coinAmount;
	[field: SerializeField] private bool        isDay;
	
	//Getters and setters
	public float CoinAmount {
		get => coinAmount;
		set => coinAmount = value;
	}
	public bool IsDay {
		get => isDay;
		set => isDay = value;
	}


	private void Awake() {
		if (Instance != null) {
			Destroy(this.gameObject);
		} else {
			Instance = this;
		}
	}
}