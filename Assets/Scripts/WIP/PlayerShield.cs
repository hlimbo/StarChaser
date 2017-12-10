using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    private AudioSource src;
    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Bullet"))
        {
            src.Play();
        }
    }
}
