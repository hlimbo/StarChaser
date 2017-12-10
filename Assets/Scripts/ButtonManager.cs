using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	public void playButtonClicked(string sceneToLoad) {
		SceneManager.LoadSceneAsync (sceneToLoad);
	}

	public void creditsButtonClicked(string sceneToLoad) {
		SceneManager.LoadSceneAsync (sceneToLoad);
	}

	public void settingsButtonClicked(string sceneToLoad) {
		SceneManager.LoadSceneAsync (sceneToLoad);
	}

	void FixedUpdate() {

	}
}
