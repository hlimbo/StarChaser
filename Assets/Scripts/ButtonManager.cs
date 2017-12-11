using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
//Nick: I don't know what you want to keep here so I'll leave these commented out for now
//I removed the lines I don't need. -- Nick

	GameObject gameObject;


	public void Start() {
		if (GameObject.Find ("main_menu_audio")) {
			AudioSource audioSource = GameObject.Find ("main_menu_audio").GetComponent<AudioSource>();
			if (!audioSource.isPlaying) {
				audioSource.Play ();
			}
			Debug.Log (audioSource);
		}

	}
	public void switchToScene(string sceneToLoad) {
            SceneManager.LoadSceneAsync (sceneToLoad);
		if (!(sceneToLoad == "Credits" || sceneToLoad == "Settings")) {
			//Destroy the audio if it's not on Credits or Settings scene
			Destroy(GameObject.Find("main_menu_audio"));
		}
	}

}
