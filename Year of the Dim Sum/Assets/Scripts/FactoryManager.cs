using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactoryManager : MonoBehaviour {
	#region Variables

	[SerializeField] public  FactorySO       SO;
	[SerializeField] private Image           image;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI earningsText;
	[SerializeField] private TextMeshProUGUI cooldownText;
	[SerializeField] private TextMeshProUGUI costText;
	[SerializeField] private Image           cooldownFillImage;
	[SerializeField] private GameObject      buttonGameObject;

	GameManager gameManager;

	private void Start() {
		gameManager       = FindAnyObjectByType<GameManager>();
		image.sprite      = SO.Sprite();
		nameText.text     = SO.Name();
		earningsText.text = SO.Earnings.ToString();
		cooldownText.text = Math.Round(SO.Cooldown(), 1).ToString();
		costText.text     = SO.Cost().ToString();
	}

	#endregion

	#region LevelUp

	public void UnlockFactory(GameObject factory) {
		factory.gameObject.SetActive(true);
		Destroy(buttonGameObject);
	}

	public void LevelUp(FactorySO factory) {
		if (gameManager.CoinAmount >= factory.Cost() && factory.Level < factory.MaxLevel()) {
			gameManager.CoinAmount -= factory.Cost();
			SO.Level++;
			costText.text = factory.Level == factory.MaxLevel() ? "Max" : SO.Cost().ToString();
			if (factory.Level         == factory.MaxLevel()) StartCoroutine(MakeAutomaticCoroutine(factory));
			if (SO.makeMoneyCoroutine != null) cooldownText.text = Math.Round(SO.Cooldown(), 1).ToString();
		}
	}

	#endregion

	#region MakeMoney

	public void MakeMoney(FactorySO factory) {
		if (factory.automatic) return;
		factory.makeMoneyCoroutine ??= StartCoroutine(MakeMoneyCoroutine(factory));
	}

	private IEnumerator MakeMoneyCoroutine(FactorySO factory) {
		float time = 0;
		while (time < factory.Cooldown()) {
			if (!factory.automatic) {
				cooldownFillImage.fillAmount = time / factory.Cooldown();
				cooldownText.text            = Math.Round(factory.Cooldown() - time, 1) + " Sec";
			}

			time += Time.deltaTime;

			yield return null;
		}

		cooldownFillImage.fillAmount =  0;
		cooldownText.text            =  Math.Round(factory.Cooldown(), 1) + " Sec";
		gameManager.CoinAmount       += factory.Earnings;
		factory.makeMoneyCoroutine   =  null;
	}

	#endregion

	#region MakeAutomatic

	private IEnumerator MakeAutomaticCoroutine(FactorySO factory) {
		factory.automatic = true;
		cooldownText.text = (Math.Round(factory.Earnings / factory.Cooldown(), 1) + " Coins / Sec");
// nåt sånt		fillImage.GetComponent<Animation>().Play();
		while (true) {
			yield return new WaitForSeconds(factory.Cooldown());
			gameManager.CoinAmount += factory.Earnings;
			yield return null;
		}
	}

	#endregion
}