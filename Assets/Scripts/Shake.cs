using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    public float shakeTimeLeft = 0.0f;
    public float shakeIntensity = 1.0f;
    private Vector3 originalRotation;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        if (shakeTimeLeft > 0.0f) {
            shakeTimeLeft -= Time.deltaTime;
            Vector3 shakeRotation = Random.insideUnitSphere * shakeIntensity;
            shakeRotation.z = 0.0f;
            transform.rotation = Quaternion.Euler(shakeRotation);
            if (shakeTimeLeft <= 0.0f) {
                transform.rotation = Quaternion.Euler(originalRotation);
            }
        }
    }

    public void shake (float duration, float intensity) {
        originalRotation = transform.rotation.eulerAngles;
        shakeTimeLeft = duration;
        shakeIntensity = intensity;
    }
}
