using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FactoryManager : MonoBehaviour {
	[SerializeField] private FactorySO[] factories;

	private bool initialUnlock;
	private bool levelUp;

	private GameManager gameManager;
	private FactorySO   factoryInstance;

	void Start() {
		gameManager = FindFirstObjectByType<GameManager>();
	}

	public void MakeMoney(FactorySO factory) {
		factoryInstance = factory;
	}

	public void MakeMoneyGraphics(GameObject factory) {
		Image fillImage = null;
		TextMeshProUGUI cooldownText = null;
		foreach (Image i in factory.GetComponentsInChildren<Image>()) {
			if (i.CompareTag("CooldownImage")) {
				fillImage = i;
				break;
			}
		}
		foreach (TextMeshProUGUI t in factory.GetComponentsInChildren<TextMeshProUGUI>()) {
			if (t.CompareTag("CooldownText")) {
				cooldownText = t;
				break;
			}
		}
		factoryInstance.makeMoneyCoroutine ??= StartCoroutine(MakeMoneyCoroutine(factoryInstance, fillImage, cooldownText));
	}

	public void ActivateFactory(GameObject factory) {
		factory.gameObject.SetActive(true);
	}
	
	public void LevelUp(FactorySO factory) {
		if (gameManager.CoinAmount >= factory.Cost() && factory.Level<factory.MaxLevel()) {
			factoryInstance        =  factory;
			levelUp                =  true;
			initialUnlock          =  factory.Level == 0;
			gameManager.CoinAmount -= factory.Cost();
			factory.Level++;
		}
	}

	
	public void LevelUpText(GameObject factoryObject) {
		StartCoroutine(LevelUpCoroutine(factoryObject));
	}

	IEnumerator LevelUpCoroutine(GameObject factoryObject) {
		yield return new WaitForEndOfFrame();
		if (initialUnlock) {
			foreach (Button b in factoryObject.GetComponentsInChildren<Button>()) {
				if (b.CompareTag("InitialUpgradeButton")) {
					Destroy(b.gameObject);
					break;
				}
			}
		}

		if (levelUp) {
			string[] tags = { "LevelText", "CostText", "EarningsText", "CooldownText" };
			string[] text = {
				(factoryInstance.Level-1).ToString(), "Cost: " + factoryInstance.Cost(),
				"Earn: " + factoryInstance.Earnings, Math.Round(factoryInstance.Cooldown(), 1) + " sec"
			};

			TextMeshProUGUI[] texts = factoryObject.GetComponentsInChildren<TextMeshProUGUI>();
			foreach (TextMeshProUGUI t in texts)
				for (int i = 0; i < tags.Length; i++) {
					if (t.CompareTag(tags[i])) {
						if (i != 3 | factoryInstance.makeMoneyCoroutine == null) {
							t.text = text[i];
							break;
						}
					}
				}
		}
	}
	private IEnumerator MakeMoneyCoroutine(FactorySO factory, Image fillImage, TextMeshProUGUI cooldownText) {
		float time = 0;
		while (time < factory.Cooldown()) {
			if (!factory.automatic) {
				fillImage.fillAmount = time / factory.Cooldown();
				cooldownText.text    = Math.Round(factory.Cooldown() - time, 1)+ " sec";
			}
			time += Time.deltaTime;
			
			yield return null;
		}

		fillImage.fillAmount       =  0;
		cooldownText.text          =  Math.Round(factory.Cooldown(), 1) + " sec";
		gameManager.CoinAmount     += factory.Earnings;
		factory.makeMoneyCoroutine =  null;
	}
	
	public void MakeAutomatic(FactorySO factory) {
		factoryInstance = factory;
	}
	public void MakeAutomaticGraphics(GameObject factory) {
		Image           fillImage    = null;
		TextMeshProUGUI cooldownText = null;
		foreach (Image i in factory.GetComponentsInChildren<Image>()) {
			if (i.CompareTag("CooldownImage")) {
				fillImage = i;
				break;
			}
		}
		foreach (TextMeshProUGUI t in factory.GetComponentsInChildren<TextMeshProUGUI>()) {
			if (t.CompareTag("CooldownText")) {
				cooldownText = t;
				break;
			}
		}
		StartCoroutine(MakeAutomaticCoroutine(factoryInstance, fillImage, cooldownText));
	}
	private IEnumerator MakeAutomaticCoroutine(FactorySO factory, Image fillImage, TextMeshProUGUI cooldownText) {
		cooldownText.text = (Math.Round(factory.Earnings / factory.Cooldown(), 1) + " coins / sec");
// nåt sånt		fillImage.GetComponent<Animation>().Play();
		while (true) {
			yield return new WaitForSeconds(factory.Cooldown());
			gameManager.CoinAmount     += factory.Earnings;
			yield return null;
		} 
		;
	}
}