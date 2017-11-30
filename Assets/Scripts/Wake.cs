using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wake : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("It's finally working");
        if (col.gameObject.tag == "Enemy")
        {
            Movement movementScript = col.gameObject.GetComponent<Movement>();
            movementScript.enabled = true;
        }
    }
}