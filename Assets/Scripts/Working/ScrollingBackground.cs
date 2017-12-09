using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollingBackground : MonoBehaviour {

    [Range(0f,1f)]
    public float scrollSpeed;
    private RawImage background;
    private Vector2 moveOffset;
    //used to restore the offset of the texture to prevent unnecessary changes to the source texture file in git
    private Vector2 originalTexOffset;

    void Start()
    {
        background = GetComponent<RawImage>();
        moveOffset = Vector2.zero;
        originalTexOffset = background.material.mainTextureOffset;
    }

    void Update ()
    {
        moveOffset.Set(0f, Mathf.Repeat((scrollSpeed * Time.deltaTime) + background.material.mainTextureOffset.y, 1f));
        background.material.mainTextureOffset = moveOffset;
    }

    void OnApplicationQuit()
    {
        //restore texture offset back to normal
        background.material.mainTextureOffset = originalTexOffset;
    }
}
