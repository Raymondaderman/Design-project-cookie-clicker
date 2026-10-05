using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	[Header("General Information")]
	[SerializeField] private float coinAmount;
	[SerializeField] private bool        isDay;
	[SerializeField] private FactorySO[] factories;

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
		foreach (FactorySO factory in factories) {
			if (factory.automatic && factory.makeMoneyCoroutine == null)
				factory.makeMoneyCoroutine = StartCoroutine(MakeMoney(factory));
		}
	}

	public IEnumerator MakeMoney(FactorySO factory) {
		yield return new WaitForSeconds(factory.Cooldown());
		
	}
}