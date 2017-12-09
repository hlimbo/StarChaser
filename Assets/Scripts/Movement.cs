using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed = 1.0f;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        if(Camera.main == GetComponent<Camera>() || GetComponent<SpriteRenderer>().isVisible)
            transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
    }
}
