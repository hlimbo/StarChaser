﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed = 1.0f;

    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void Update () {
        transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
    }
}
