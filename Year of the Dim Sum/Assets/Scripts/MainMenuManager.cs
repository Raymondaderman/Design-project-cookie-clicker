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
		
	}


	public void OnStartClick() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	public void OnQuitClick() {
		Application.Quit();
	}
}