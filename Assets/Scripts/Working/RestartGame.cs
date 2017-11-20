using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

    public string sceneToReload = "TouchControlsFinal";

    public void Restart()
    {
        SceneManager.LoadScene(sceneToReload);
    }
}
