using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Transform transform;
	private Camera camera;

	public float speed = 13.0f;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
		camera = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			Debug.Log (camera.ScreenToWorldPoint (Input.mousePosition));
			Vector3 position = camera.ScreenToWorldPoint (Input.mousePosition);
			position.z = 0.0f;
			transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
		}
	}
}
