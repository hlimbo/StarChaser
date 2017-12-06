using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakness : MonoBehaviour {

    public string[] weaknessTags;

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(string weaknessTag in weaknessTags)
        {
            if (collision.tag.Equals(weaknessTag))
            {
                //Debug.Log("weakness touched: " + collision.tag);
                //Debug.Log(collision.gameObject.name);
                Destroy(this.gameObject);

                //TODO: implement your own functionality once weakness by some object is triggered
                break;
            }
        }
    }
}
