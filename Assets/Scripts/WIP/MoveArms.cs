using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArms : MonoBehaviour {

    public float speed = 0f;

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
    }
}
