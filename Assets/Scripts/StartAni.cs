using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAni : MonoBehaviour {
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {
//        if (transform.position.y <= Camera.main.transform.position.y + Camera.main.orthographicSize + 0.125f || Camera.main.GetComponent<Movement>().speed == 0f)
//            anim.Play();
    }
}
