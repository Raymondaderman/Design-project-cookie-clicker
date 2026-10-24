using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	[Header("General Information")]
	[SerializeField] private float coinAmount;
	[SerializeField] private bool isDay;

	[Header("TMP")]
	[SerializeField] private TextMeshProUGUI coinDisplay;

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

	private void Update() {
		//coinDisplay.text="Coins: " + coinAmount;	
	}
}

