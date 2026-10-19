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
		gameManager = FindFirstObjectByType<GameManager>();
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
					StartCoroutine(MakeMoneyCoroutine(factory));
			}
		}
	}

	public void MakeMoney(FactorySO factory) {
		print("1");
		factory.makeMoneyCoroutine ??= StartCoroutine(MakeMoneyCoroutine(factory));
	}

	private FactorySO factoryInstance;
	public void LevelUp(FactorySO factory) {
		if (gameManager.CoinAmount >= factory.Cost()) {
			gameManager.CoinAmount -= factory.Cost();
			factory.Level++;
			factoryInstance = factory;
		}
	}
	public void LevelUpText(GameObject factoryObject) {
		StartCoroutine(LevelUpCoroutine(factoryObject));
	}

	IEnumerator LevelUpCoroutine(GameObject factoryObject) {
		yield return new WaitForEndOfFrame();
		string[] tags = { "LevelText", "CostText", "EarningsText", "CooldownText" };
		string[] text = {
			factoryInstance.Level.ToString(), factoryInstance.Cost().ToString(), factoryInstance.Earnings.ToString(),
			factoryInstance.Cooldown().ToString()
		};

		TextMeshProUGUI[] texts = factoryObject.GetComponentsInChildren<TextMeshProUGUI>();
		foreach (TextMeshProUGUI t in texts)
			for (int i = 0; i < tags.Length; i++) {
				if (t.CompareTag(tags[i])) {
					t.text = text[i];
					break;
				}
			}
	}

	private IEnumerator MakeMoneyCoroutine(FactorySO factory) {
		float time = 0;
		print("2");
		while (time < factory.Cooldown()) {
			time                 += Time.deltaTime;
			print(time);
			yield return null;
		}

		gameManager.CoinAmount     += factory.Earnings;
		factory.makeMoneyCoroutine =  null;
	}
}
