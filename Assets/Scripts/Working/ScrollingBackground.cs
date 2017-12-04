using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollingBackground : MonoBehaviour {

    [Range(0f,1f)]
    public float scrollSpeed;
    private RawImage background;
    private Vector2 moveOffset;

    void Start()
    {
        background = GetComponent<RawImage>();
        moveOffset = Vector2.zero;
    }

    void Update ()
    {
        moveOffset.Set(0f, Mathf.Repeat((scrollSpeed * Time.deltaTime) + background.material.mainTextureOffset.y, 1f));
        background.material.mainTextureOffset = moveOffset;
    }
}
