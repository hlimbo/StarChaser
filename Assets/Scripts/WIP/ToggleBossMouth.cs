using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used as an animation event
public class ToggleBossMouth : MonoBehaviour {

    private Collider2D hitbox;

    void Awake()
    {
        hitbox = GameObject.Find("Mouth").GetComponent<Collider2D>();
    }

    public void OpenMouth()
    {
        hitbox.enabled = true;
    }

    public void CloseMouth()
    {
        hitbox.enabled = false;
    }
}
