using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour {
    //Attach this component to any GameObject that has a Movement component.
    public float acceleration = 0.0f;
    private Movement movement;

    // Use this for initialization
    void Start () {
        movement = GetComponent<Movement> ();
    }
    
    // Update is called once per frame
    void Update () {
        movement.speed += acceleration * Time.deltaTime;
    }
}
