using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventDestruction : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		
	}

	void Start() {
		DontDestroyOnLoad(GetComponent<AudioSource>());
	}
}
