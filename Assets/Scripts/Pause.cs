using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	public GameObject pauseObj;
	private float tempTimeScale;

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale != 0) {
				tempTimeScale = Time.timeScale;
			}
			PauseGame();
		}
	}

	void PauseGame() {
		pauseObj.SetActive(!pauseObj.activeInHierarchy);
		if (Time.timeScale != 0) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = tempTimeScale;
		}
	}

	public void ResumeButton() {
		PauseGame();
	}

	public void MenuExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
	}

	public void ExitButton() {
		Application.Quit();
	}
}
