using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used as an animation event
public class ToggleBossMouth : MonoBehaviour {

    [SerializeField]
    private GameObject mouth;

    void Awake()
    {
        mouth = GameObject.Find("Mouth");
        mouth.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OpenMouth()
    {
        mouth.GetComponent<BoxCollider2D>().enabled = true;
        mouth.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void CloseMouth()
    {
        mouth.GetComponent<BoxCollider2D>().enabled = false;
        mouth.GetComponent<SpriteRenderer>().enabled = false;
    }
}
