using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCleanUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= Camera.main.transform.position.y - Camera.main.orthographicSize + 0.125f)
            Destroy(this.gameObject);
    }
}
