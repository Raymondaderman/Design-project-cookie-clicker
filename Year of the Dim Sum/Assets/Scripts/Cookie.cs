using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Cookie : MonoBehaviour {
	[SerializeField] private float     timeToDestroy;
	[SerializeField]         AudioClip fortunCookieSound;

	[SerializeField] private GameObject powerUpPopupPrefab;
	private                  float      timer;

	public void OnFortunCookieClick() {
		Debug.Log("Click cookie");
		AudioManager.instance.PlayASound(fortunCookieSound, 1f);
		FindAnyObjectByType<PowerUpManager>()
			.StartCoroutine(PowerUpManager.Instance.FortuneCookiePowerUp(powerUpPopupPrefab));

		Destroy(gameObject);
	}

	private void FixedUpdate() {
		timer += Time.deltaTime;
		if (timer > timeToDestroy) Destroy(gameObject);
	}
}