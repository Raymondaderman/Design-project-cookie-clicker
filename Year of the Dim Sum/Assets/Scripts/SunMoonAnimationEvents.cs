using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SunMoonAnimationEvents : MonoBehaviour {
	[SerializeField] public float tintChangeTime;
	[SerializeField] public float lightIntensity;

	[SerializeField] private CanvasGroup backgroundTintCanvasGroup;

	[SerializeField] private GameObject[] lanterns;
	[SerializeField] private Light2D[]    lights;

	private void Awake() {
		lanterns = GameObject.FindGameObjectsWithTag("Lantern");
		lights   = new Light2D[lanterns.Length];
		for (var i = 0; i < lanterns.Length; i++) {
			lights[i] = lanterns[i].GetComponentInChildren<Light2D>();
		}
	}

	public void IsDayFalseEvent() {
		Debug.Log("Night Start");
		GameManager.Instance.IsDay = false;
		LeanTween.alphaCanvas(backgroundTintCanvasGroup, 1, tintChangeTime).setEaseOutExpo();
		foreach (var light in lights) {
			LeanTween.value(gameObject, 0, lightIntensity, tintChangeTime).setEaseOutExpo()
			         .setOnUpdate(val => { light.intensity = val; });
		}
	}

	public void IsDayTrueEvent() {
		Debug.Log("Day Start");
		GameManager.Instance.IsDay = true;
		LeanTween.alphaCanvas(backgroundTintCanvasGroup, 0, tintChangeTime).setEaseOutExpo();
		foreach (var light in lights) {
			LeanTween.value(gameObject, lightIntensity, 0, tintChangeTime).setEaseOutExpo()
			         .setOnUpdate(val => { light.intensity = val; });
		}
	}
}