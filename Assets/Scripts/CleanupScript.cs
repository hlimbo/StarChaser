using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupScript : MonoBehaviour
{

    //ATTACH THIS TO A BOX FOLLOWING CAMERA

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit(Collider2D other)
    {
        Destroy(other.gameObject.transform.parent.gameObject);
    }
}
