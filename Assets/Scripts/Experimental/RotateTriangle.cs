using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTriangle : MonoBehaviour {

    public float angularVelocity = 45.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {
        rb.MoveRotation(rb.rotation + angularVelocity * Time.deltaTime);
        //transform.Rotate(Vector3.forward, 30.0f * Time.deltaTime);
    }
}
