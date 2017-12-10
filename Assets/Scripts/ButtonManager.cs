using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	public void switchToScene(string sceneToLoad) {
		SceneManager.LoadSceneAsync (sceneToLoad);
	}
	public void creditsGoBack(string sceneToLoad) {
		SceneManager.LoadSceneAsync (sceneToLoad);
		Debug.Log ("clcik");
	}
}
