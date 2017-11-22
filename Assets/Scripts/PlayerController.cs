using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 13.0f;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0)) {
            Vector3 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            position.z = 0.0f;
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
        }
    }
}
