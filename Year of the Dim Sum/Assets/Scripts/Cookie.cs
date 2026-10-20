using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Cookie : MonoBehaviour {
	[SerializeField] private float     timeToDestroy;
	[SerializeField] private AudioClip fortuneCookieSound;

	[SerializeField] private GameObject powerUpPopupPrefab;
	[SerializeField] private Animator   fortuneCookieAnimator;
	private                  float      timer;

	private void Awake() {
		fortuneCookieAnimator = GetComponent<Animator>();
	}

	public void OnFortunCookieClick() {
		Debug.Log("Click cookie");
		AudioManager.Instance.PlayASound(fortuneCookieSound, 1f);
		powerUpPopupPrefab.transform.position = gameObject.transform.position;
		FindAnyObjectByType<PowerUpManager>()
			.StartCoroutine(PowerUpManager.Instance.FortuneCookiePowerUp(powerUpPopupPrefab));

		Destroy(gameObject);
	}

	private void FixedUpdate() {
		timer += Time.deltaTime;
		if (timer > timeToDestroy) fortuneCookieAnimator.SetTrigger("FadeOut");
	}

	public void DestroyCookieEvent() {
		Destroy(gameObject);
	}
}