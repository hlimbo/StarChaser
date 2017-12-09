using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotations : MonoBehaviour {

    public GameObject center;
    public float rotateSpeed = 120.0f;
    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        transform.RotateAround(center.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
