using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour {

	private Button startButton;
	private Button quitButton;

	private void Start() {
		var root = GetComponent<UIDocument>().rootVisualElement;
		
		startButton = root.Q<Button>("StartButton");
		quitButton = root.Q<Button>("QuitButton");
		
		startButton.RegisterCallback<ClickEvent>(OnStartClick);
		quitButton.RegisterCallback<ClickEvent>(OnQuitClick);
		
	}


	private void OnStartClick(ClickEvent clickEvent) {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void OnQuitClick(ClickEvent clickEvent) {
		Application.Quit();
	}
}