using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BulletKiller : MonoBehaviour {

    private BoxCollider2D entityBounds;

    void Start () {
        entityBounds = GetComponent<BoxCollider2D>();
        entityBounds.size = new Vector2(Camera.main.orthographicSize * Camera.main.aspect * 2.5f, Camera.main.orthographicSize * 2.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
