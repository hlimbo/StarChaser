using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
//Nick: I don't know what you want to keep here so I'll leave these commented out for now
//I removed the lines I don't need. -- Nick
public void switchToScene(string sceneToLoad) {
	        SceneManager.LoadSceneAsync (sceneToLoad);
}

}
