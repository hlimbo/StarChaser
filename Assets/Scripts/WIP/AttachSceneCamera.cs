using UnityEngine;
using UnityEngine.UI;

public class AttachSceneCamera : MonoBehaviour {

    private Canvas cameraCanvas;   
    void Awake()
    {
        cameraCanvas = GetComponent<Canvas>();
        //Camera.main is the SceneCamera gameobject
        cameraCanvas.worldCamera = Camera.main;
    }
}
