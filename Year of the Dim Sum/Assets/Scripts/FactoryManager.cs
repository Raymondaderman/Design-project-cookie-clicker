using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class FactoryManager : MonoBehaviour
{
	[SerializeField] private FactorySO[] factories;
	[SerializeField] private Slider[] cooldownSliders;
	
	GameManager gameManager;
	
	void Start() {
		gameManager = GetComponent<GameManager>();
	}

	private void Update() {
		foreach (FactorySO factory in factories) {
			if (factory.automatic) {
				int sliderIndex = 0;
				for (int i = 0; i < factories.Length; i++) {
					if (factories[i] == factory) {
						sliderIndex = i;
						break;
					}
				}
				factory.makeMoneyCoroutine ??=
					StartCoroutine(MakeMoneyCoroutine(factory, cooldownSliders[sliderIndex]));
			}
		}
	}

	public void MakeMoney(FactorySO factory, Slider timer) {
		if (factory.automatic) factory.makeMoneyCoroutine ??= StartCoroutine(MakeMoneyCoroutine(factory, timer));
	}

	public void LevelUp(FactorySO factory, TextMeshProUGUI levelText, TextMeshProUGUI costText,  TextMeshProUGUI earningsText, TextMeshProUGUI cooldownText) {
		if (gameManager.CoinAmount >= factory.Cost()) {
			gameManager.CoinAmount -= factory.Cost();
			factory.Level++;
			levelText.text = factory.Level.ToString();
			costText.text  = factory.Cost().ToString();
			earningsText.text = factory.Earnings().ToString();
			cooldownText.text = factory.Cooldown().ToString();
		}
	}

	private IEnumerator MakeMoneyCoroutine(FactorySO factory, Slider cooldownSlider) {
		float time = 0;
		while (time < factory.Cooldown()) {
			time                 += Time.deltaTime;
			cooldownSlider.value =  time / factory.Cooldown();
			yield return null;
		}

		gameManager.CoinAmount     += factory.Earnings();
		factory.makeMoneyCoroutine =  null;
	}
}
