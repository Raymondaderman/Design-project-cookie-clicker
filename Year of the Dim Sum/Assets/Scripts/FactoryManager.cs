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

	public void LevelUp() {
		if (gameManager.CoinAmount >= SO.Cost() && SO.Level < SO.MaxLevel()) {
			gameManager.CoinAmount -= SO.Cost();
			this.SO.Level++;
			costText.text = SO.Level == SO.MaxLevel()
				                ? "Max"
				                : GameManager.Instance.ConvertedNumber(this.SO.Cost());
			if (SO.Level                   == SO.MaxLevel()) StartCoroutine(MakeAutomaticCoroutine());
			if (this.SO.makeMoneyCoroutine != null) cooldownText.text = Math.Round(this.SO.Cooldown(), 1).ToString();
		}
	}

	#endregion

	#region MakeMoney

	public void MakeMoney() {
		Debug.Log("Make Money Pressed");
		if (SO.automatic) return;
		SO.makeMoneyCoroutine ??= StartCoroutine(MakeMoneyCoroutine());
	}

	private IEnumerator MakeMoneyCoroutine() {
		float time = 0;
		while (time < SO.Cooldown()) {
			if (!SO.automatic) {
				cooldownFillImage.fillAmount = time / SO.Cooldown();
				cooldownText.text            = Math.Round(SO.Cooldown() - time, 1) + " Sec";
			}

			time += Time.deltaTime;

			yield return null;
		}

		cooldownFillImage.fillAmount =  0;
		cooldownText.text            =  Math.Round(SO.Cooldown(), 1) + " Sec";
		gameManager.CoinAmount       += SO.Earnings;
		SO.makeMoneyCoroutine   =  null;
	}

	#endregion

	#region MakeAutomatic

	private IEnumerator MakeAutomaticCoroutine() {
		SO.automatic            = true;
		cooldownText.text            = (Math.Round(SO.Earnings / SO.Cooldown(), 1) + " Coins / Sec");
		cooldownFillImage.fillAmount = 100;
		while (true) {
			cooldownFillImage.sprite = progressBarSprite1;
			yield return new WaitForSeconds(SO.Cooldown());
			cooldownFillImage.sprite =  progressBarSprite2;
			gameManager.CoinAmount   += SO.Earnings;
			yield return null;
		}
	}

	#endregion
}