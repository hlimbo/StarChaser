using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
//Nick: I don't know what you want to keep here so I'll leave these commented out for now
//I removed the lines I don't need. -- Nick

    GameObject gameObject;



    public void switchToScene(string sceneToLoad) {
        if (GameObject.Find ("button_beep")) {
            DontDestroyOnLoad (GameObject.Find ("button_beep").GetComponent<AudioSource> ());
            GameObject.Find ("button_beep").GetComponent<AudioSource> ().Play ();
        }

        SceneManager.LoadSceneAsync (sceneToLoad);
        if (!(sceneToLoad == "Credits" || sceneToLoad == "Settings" || sceneToLoad == "MainMenu")) {
            //Destroy the audio if we're going to play the game
            Destroy(GameObject.Find("main_menu_audio"));
            Destroy(GameObject.Find("button_beep"));

        }
    }

}
