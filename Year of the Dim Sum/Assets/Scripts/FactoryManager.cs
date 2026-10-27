using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactoryManager : MonoBehaviour {
	[SerializeField] public  FactorySO       SO;
	[SerializeField] private SpriteRenderer  spriteRenderer;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI earningsText;
	[SerializeField] private TextMeshProUGUI cooldownText;
	[SerializeField] private TextMeshProUGUI costText;
	[SerializeField] private Image           cooldownFillImage;

	GameManager gameManager;

	private void Start() {
		gameManager           = FindAnyObjectByType<GameManager>();
		spriteRenderer.sprite = SO.Sprite();
		nameText.text         = SO.Name();
		earningsText.text     = SO.Earnings.ToString();
		cooldownText.text     = Math.Round(SO.Cooldown(), 1).ToString();
		costText.text         = SO.Cost().ToString();
	}

	public void ActivateFactory(GameObject factory) {
		factory.gameObject.SetActive(true);
	}

	public void DeactivateButton(GameObject button) {
		Destroy(button);
	}

	public void LevelUp(FactorySO factory) {
		if (gameManager.CoinAmount >= factory.Cost() && factory.Level < factory.MaxLevel()) {
			gameManager.CoinAmount -= factory.Cost();
			SO.Level++;
			costText.text = SO.Cost().ToString();
			if (SO.makeMoneyCoroutine != null) cooldownText.text = Math.Round(SO.Cooldown(), 1).ToString();
		}
	}

	#region MakeMoney

	public void MakeMoney(FactorySO factory) {
		factory.makeMoneyCoroutine ??= StartCoroutine(MakeMoneyCoroutine(factory));
	}

	private IEnumerator MakeMoneyCoroutine(FactorySO factory) {
		float time = 0;
		while (time < factory.Cooldown()) {
			if (!factory.automatic) {
				cooldownFillImage.fillAmount = time / factory.Cooldown();
				cooldownText.text            = Math.Round(factory.Cooldown() - time, 1) + " sec";
			}

			time += Time.deltaTime;

			yield return null;
		}

		cooldownFillImage.fillAmount =  0;
		cooldownText.text            =  Math.Round(factory.Cooldown(), 1) + " sec";
		gameManager.CoinAmount       += factory.Earnings;
		factory.makeMoneyCoroutine   =  null;
	}

	#endregion

	#region MakeAutomatic

	public void MakeAutomatic() {
		StartCoroutine(MakeAutomaticCoroutine(SO));
	}

	private IEnumerator MakeAutomaticCoroutine(FactorySO factory) {
		cooldownText.text = (Math.Round(factory.Earnings / factory.Cooldown(), 1) + " coins / sec");
// nåt sånt		fillImage.GetComponent<Animation>().Play();
		while (true) {
			yield return new WaitForSeconds(factory.Cooldown());
			gameManager.CoinAmount += factory.Earnings;
			yield return null;
		}

		#endregion
	}
}
