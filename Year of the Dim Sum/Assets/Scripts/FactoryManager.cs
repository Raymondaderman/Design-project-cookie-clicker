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

	[SerializeField] private Sprite progressBarSprite1;
	[SerializeField] private Sprite progressBarSprite2;

	GameManager gameManager;

	private void Start() {
		gameManager       = FindAnyObjectByType<GameManager>();
		image.sprite      = SO.Sprite();
		nameText.text     = SO.Name();
		earningsText.text = GameManager.Instance.ConvertedNumber(SO.Earnings);
		cooldownText.text = Math.Round(SO.Cooldown(), 1).ToString();
		costText.text     = GameManager.Instance.ConvertedNumber(SO.Cost());

		var unlockCost                  = buttonGameObject.GetComponentInChildren<TextMeshProUGUI>();
		unlockCost.text = costText.text = GameManager.Instance.ConvertedNumber(SO.UnlockCost);
	}

	#endregion

	#region LevelUp

	public void UnlockFactory(GameObject unlockHolder) {
		if (GameManager.Instance.CoinAmount < SO.UnlockCost) return;
		GameManager.Instance.CoinAmount -= SO.UnlockCost;
		Destroy(unlockHolder);
	}

	public void LevelUp(FactorySO factory) {
		if (gameManager.CoinAmount >= factory.Cost() && factory.Level < factory.MaxLevel()) {
			gameManager.CoinAmount -= factory.Cost();
			SO.Level++;
			costText.text = factory.Level == factory.MaxLevel()
				                ? "Max"
				                : GameManager.Instance.ConvertedNumber(SO.Cost());
			if (factory.Level         == factory.MaxLevel()) StartCoroutine(MakeAutomaticCoroutine(factory));
			if (SO.makeMoneyCoroutine != null) cooldownText.text = Math.Round(SO.Cooldown(), 1).ToString();
		}
	}

	#endregion

	#region MakeMoney

	public void MakeMoney(FactorySO factory) {
		Debug.Log("Make Money Pressed");
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
		factory.automatic            = true;
		cooldownText.text            = (Math.Round(factory.Earnings / factory.Cooldown(), 1) + " Coins / Sec");
		cooldownFillImage.fillAmount = 100;
		while (true) {
			cooldownFillImage.sprite = progressBarSprite1;
			yield return new WaitForSeconds(factory.Cooldown());
			cooldownFillImage.sprite =  progressBarSprite2;
			gameManager.CoinAmount   += factory.Earnings;
			yield return null;
		}
	}

	#endregion
}