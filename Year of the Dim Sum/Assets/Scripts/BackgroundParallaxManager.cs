using UnityEngine;
using UnityEngine.InputSystem;

public class BackgroundParallaxManager : MonoBehaviour {
	[SerializeField] private GameObject[] parallaxObjects;
	[SerializeField] private float        mouseSpeedX;
	[SerializeField] private float        mouseSpeedY;

	private Vector2   mousePosition;
	private Vector2[] originalPositions;

	private void Start() {
		Cursor.lockState = CursorLockMode.Confined;

		originalPositions = new Vector2[parallaxObjects.Length];
		for (var i = 0; i < parallaxObjects.Length; i++) {
			originalPositions[i] = parallaxObjects[i].transform.position;
		}
	}

	private void FixedUpdate() {
		var centeredMousePosX = (mousePosition.x - (Screen.width / 2f)) * (mouseSpeedX / Screen.width);
		var centeredMousePosY = (mousePosition.y - (Screen.width / 2f)) * (mouseSpeedY / Screen.width);


		for (var i = 0; i < parallaxObjects.Length; i++) {
			parallaxObjects[i].transform.position = originalPositions[i] +
			                                        new Vector2(centeredMousePosX, centeredMousePosY) *
			                                        (i * i);
		}
	}

	private void OnMousePosition(InputValue value) {
		mousePosition = value.Get<Vector2>();
	}
}