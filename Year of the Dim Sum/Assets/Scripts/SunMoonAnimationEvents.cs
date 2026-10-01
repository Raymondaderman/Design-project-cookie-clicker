using UnityEngine;

public class SunMoonAnimationEvents : MonoBehaviour {
	[SerializeField] public  float       tintChangeTime;
	[SerializeField] private CanvasGroup backgroundTintCanvasGroup;

	public void IsDayFalseEvent() {
		GameManager.Instance.IsDay = false;
		LeanTween.alphaCanvas(backgroundTintCanvasGroup, 1, tintChangeTime).setEaseOutExpo();
	}

	public void IsDayTrueEvent() {
		GameManager.Instance.IsDay = true;
		LeanTween.alphaCanvas(backgroundTintCanvasGroup, 0, tintChangeTime).setEaseOutExpo();
	}
}