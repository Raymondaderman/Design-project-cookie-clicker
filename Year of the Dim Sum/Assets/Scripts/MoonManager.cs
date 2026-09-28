using UnityEngine;

public class MoonManager : MonoBehaviour {
	[SerializeField] private float moonClickAmount;


	public void OnClick() {
		GameManager.Instance.CoinAmount += moonClickAmount;
		Debug.Log("Hej");
	}
}