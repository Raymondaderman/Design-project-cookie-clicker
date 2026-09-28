using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SunMoonAnimationEvents : MonoBehaviour {
	[SerializeField] public  float tintChangeTime;
	[SerializeField] private Image backgroundTint;

	public void IsDayFalseEvent() {
		GameManager.Instance.IsDay = false;
		//StartCoroutine(BackgroundTintChanger(0f, 225f));
	}

	public void IsDayTrueEvent() {
		GameManager.Instance.IsDay = true;
		//StartCoroutine(BackgroundTintChanger(225f, 0f));
	}

	/*private IEnumerator BackgroundTintChanger(float fromValue, float toValue) {
		var tintChangeTimer = tintChangeTime;
		while (tintChangeTimer > 0f) {
			tintChangeTimer            -= Time.deltaTime;
			backgroundTint.tintColor.a =  Mathf.Lerp(fromValue, toValue, tintChangeTimer / tintChangeTime);
			yield return null;
		}

		yield return null;
	}*/
}