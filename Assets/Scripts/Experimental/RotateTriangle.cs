using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTriangle : MonoBehaviour {

    public float angularVelocity = 45.0f;

	// Update is called once per frame
	void Update () {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.MoveRotation(rb.rotation + angularVelocity * Time.deltaTime);
        //transform.Rotate(Vector3.forward, 30.0f * Time.deltaTime);
	}
}
