using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpriteSize : MonoBehaviour {

    private SpriteRenderer butt;
    private SpriteRenderer body;
    private SpriteRenderer tip;

    private CapsuleCollider2D hitbox;

    //how far away laser tip is from laser body
    public float y_offset = 0.04f;

    private float bodyYScale;
    // Use this for initialization
    void Awake () {
        Debug.Log(transform.childCount);

        butt = transform.Find("laser_butt").GetComponent<SpriteRenderer>();
        body = transform.Find("laser_body").GetComponent<SpriteRenderer>();
        tip = transform.Find("laser_tip").GetComponent<SpriteRenderer>();
        hitbox = transform.Find("LaserHitbox").GetComponent<CapsuleCollider2D>();

        Debug.Log(hitbox.size);
        bodyYScale = body.transform.localScale.y;

        SetY(tip.transform, LaserTipYPos);

        //update laser hitbox height
        float oldHitboxHeight = hitbox.size.y;
        hitbox.size = new Vector2(hitbox.size.x, LaserHeight);

        float deltaY = (LaserHeight - oldHitboxHeight) / 2f;

        Debug.Log("laser height: " + LaserHeight);

        //update laser hitbox y offset
        SetY(hitbox.transform, hitbox.transform.localPosition.y + deltaY);

        bodyYScale = body.transform.localScale.y;

        //use sr.sprite.bounds.size to get the size of each sprite in unity units
        //use sr.sprite.rect.size to get the size of each sprite in pixels
        //sr.sprite.bounds.size.x * transform.localScale.x gives us the scaled width of the sprite in unity units
        //sr.sprite.bounds.size.y * transform.localScale.y gives us the scaled height of the sprite in unity units
    }
    
    // Update is called once per frame
    void Update () {


        if (body.transform.localScale.y != bodyYScale)
        {
            Debug.Log(hitbox.size);
            bodyYScale = body.transform.localScale.y;

            SetY(tip.transform, LaserTipYPos);

            //update laser hitbox height
            float oldHitboxHeight = hitbox.size.y;
            hitbox.size = new Vector2(hitbox.size.x, LaserHeight);

            float deltaY = (LaserHeight - oldHitboxHeight) / 2f;

            Debug.Log("laser height: " + LaserHeight);

            //update laser hitbox y offset
            SetY(hitbox.transform, hitbox.transform.localPosition.y + deltaY);

            bodyYScale = body.transform.localScale.y;
        }
    }

    public float LaserHeight
    {
        get
        {
            float bodyHeight = body.sprite.bounds.size.y * body.transform.localScale.y;
            float buttHeight = butt.sprite.bounds.size.y * butt.transform.localScale.y;
            float tipHeight = tip.sprite.bounds.size.y * tip.transform.localScale.y;
            return bodyHeight + buttHeight + tipHeight;
        }
    }

    public float LaserTipYPos
    {
        get
        {
            float bodyHeight = body.sprite.bounds.size.y * body.transform.localScale.y;
            float buttHeight = butt.sprite.bounds.size.y * butt.transform.localScale.y;
            return bodyHeight + buttHeight - y_offset;
        }
    }

    private void SetY(Transform t,float newY)
    {
        t.localPosition = new Vector3(t.localPosition.x, newY, t.localPosition.z);
    }
}
