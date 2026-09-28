using UnityEngine;

public class SunMoonAnimationEvents : MonoBehaviour
{
	public void IsDayFalseEvent() {
		GameManager.Instance.IsDay = false;
	}
	public void IsDayTrueEvent() {
		GameManager.Instance.IsDay = true;
	}
}
