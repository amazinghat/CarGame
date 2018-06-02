using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctionality : MonoBehaviour {

	public Light redLight;
	public Light blueLight;
	public float lightDelay;
	private float delay;

	void Start() {
		delay = lightDelay;
		redLight.enabled = true;
		blueLight.enabled = false;
	}

	void Update() {
		delay -= Time.deltaTime;
		if (delay <= 0) {
			redLight.enabled = !redLight.enabled;
			blueLight.enabled = !blueLight.enabled;
			delay = lightDelay;
		}
	}

	public void StartButton() {
		SceneManager.LoadScene(0);
	}

	public void HighScoresButton() {
		//SceneManager.LoadScene(2);
	}

	public void OptionsButton() {
		//SceneManager.LoadScene(3);
	}

	public void ExitButton() {
		Application.Quit();
	}
}
