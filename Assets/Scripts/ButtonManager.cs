using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
//Nick: I don't know what you want to keep here so I'll leave these commented out for now
//<<<<<<< HEAD
//    public void switchToScene(string sceneToLoad) {
//        Debug.Log ("clcik");
//        SceneManager.LoadSceneAsync (sceneToLoad);
//    }
//    public void creditsGoBack(string sceneToLoad) {
//        SceneManager.LoadSceneAsync (sceneToLoad);
//        Debug.Log ("clcik");
//    }
//=======
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
//>>>>>>> 8f8a8c9d9ebfa66774a8502e306a96fe3eb5318f
}
