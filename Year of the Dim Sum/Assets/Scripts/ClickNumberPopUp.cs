using System;
using TMPro;
using UnityEngine;

public class ClickNumberPopUp : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI coinText;

	private void Start() {
		coinText.text = GameManager.Instance.ConvertedNumber(SunMoonManager.Instance.ClickAmount);
	}
}