using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakness : MonoBehaviour {

    public string[] weaknessTags;

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(string weaknessTag in weaknessTags)
        {
            if (collision.tag == weaknessTag)
            {
                print("weakness touched: " + collision.tag);
                Destroy(this.gameObject);
                break;
            }
        }
    }
}
