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

	public void MakeMoney(GameObject factoryObject) {
		FactorySO factory = factoryObject.GetComponent<FactorySO>();
		Slider cooldownSlider = factoryObject.GetComponent<Slider>();
		if (factory.automatic) factory.makeMoneyCoroutine ??= StartCoroutine(MakeMoneyCoroutine(factory, cooldownSlider));
	}

	public void LevelUp(GameObject factoryObject, TextMeshProUGUI costText,  TextMeshProUGUI earningsText, TextMeshProUGUI cooldownText) {
		FactorySO factory = factoryObject.GetComponent<FactorySO>();
		if (gameManager.CoinAmount >= factory.Cost()) {
			gameManager.CoinAmount -= factory.Cost();
			factory.Level++;
			string[] tag  = new[] { "123" };
			string[] text = new[] { "123" };
			FixText(factoryObject, tag, text);
			
			
			costText.text  = factory.Cost().ToString();
			earningsText.text = factory.Earnings.ToString();
			cooldownText.text = factory.Cooldown().ToString();
		}
	}

	private void FixText(GameObject obj, string[] tag, string[] text) {
		TextMeshProUGUI[] texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
		for (int i = 0; i < texts.Length; i++) {
			
		}
	}

	private IEnumerator MakeMoneyCoroutine(FactorySO factory, Slider cooldownSlider) {
		float time = 0;
		while (time < factory.Cooldown()) {
			time                 += Time.deltaTime;
			cooldownSlider.value =  time / factory.Cooldown();
			yield return null;
		}

		gameManager.CoinAmount     += factory.Earnings;
		factory.makeMoneyCoroutine =  null;
	}
}
