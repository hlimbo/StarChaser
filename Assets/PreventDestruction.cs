using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventDestruction : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		
	}


	public void Awake() {

		if (GameObject.FindGameObjectsWithTag ("main_menu_audio").Length == 1) {

			AudioSource audioSource = GetComponent<AudioSource> ();
			DontDestroyOnLoad (GetComponent<AudioSource> ());
			audioSource.Play ();


		}

	}
}
 