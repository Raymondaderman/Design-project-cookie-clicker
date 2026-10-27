using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	[Header("General Information")]
	[SerializeField] private float coinAmount;
	[SerializeField] private bool isDay;

	[SerializeField] private Vector2 mouseVector;

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
	public Vector2 MouseVector => mouseVector;


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

	private void OnMousePosition(InputValue value) {
		mouseVector = value.Get<Vector2>();
	}
}