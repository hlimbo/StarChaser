using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//destroy gameObject when it leaves orthographic camera view
public class DestroyProjectile : MonoBehaviour {

    [SerializeField]
    private float camHalfWidth;
    [SerializeField]
    private float camHalfHeight;
    private Camera mainCam;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
        camHalfWidth = (mainCam.orthographicSize * 2 * mainCam.aspect) / 2f;
        camHalfHeight = mainCam.orthographicSize;
	}

    // Update is called once per frame
    void Update() {

        //If this gameObject is not in camera view, destroy this gameObject
        Vector3 camPosition = mainCam.transform.position;
        if (transform.position.x > camPosition.x + camHalfWidth || transform.position.x < camPosition.x - camHalfWidth
            || transform.position.y > camPosition.y + camHalfHeight || transform.position.y < camPosition.y - camHalfHeight)
        {
            Destroy(this.gameObject);
        }
	}
}
