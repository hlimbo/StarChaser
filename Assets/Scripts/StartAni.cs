using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAni : MonoBehaviour {
    public Animator anim;

    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void Update () {
        if (transform.position.y <= Camera.main.transform.position.y + Camera.main.orthographicSize + 0.125f || Camera.main.GetComponent<Movement>().speed == 0f)
               anim.SetBool("In Camera", true);
    }
}
