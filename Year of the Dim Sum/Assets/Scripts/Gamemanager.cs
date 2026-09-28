using UnityEngine;

public class Gamemanager : MonoBehaviour {
	[SerializeField] private float coinAmount;

	public float CoinAmount {
		get => coinAmount;
		set => coinAmount += value;
	}
}