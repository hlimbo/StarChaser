using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAbility : MonoBehaviour {

    [SerializeField]
    public GameObject butt, body, tip;
    [SerializeField]
    private float y_offset = 0.04f;

    [Tooltip("Where the raycast begins from")]
    public Transform originPoint;

    private void Awake()
    {
        butt = transform.Find("laser_butt").gameObject;
        body = transform.Find("laser_body").gameObject;
        tip = transform.Find("laser_tip").gameObject;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, Vector2.up);
        if (hit.collider != null)
        {
            float hitY = hit.collider.gameObject.transform.position.y;
            tip.transform.position = new Vector3(tip.transform.position.x, hitY, tip.transform.position.z);
            float newHeight = tip.transform.localPosition.y - (butt.transform.localPosition.y + butt.GetComponent<SpriteRenderer>().sprite.bounds.size.y * butt.transform.localScale.y);
            float newYScale = (newHeight + y_offset) / body.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            body.transform.localScale = new Vector3(body.transform.localScale.x, newYScale, body.transform.localScale.z);
        }
        else
        {
            //stretch past the screen vertically by rescaling the body's sprite
            float newTipYPos = (Camera.main.orthographicSize * 2.5f) + Camera.main.transform.position.y;
            tip.transform.position = new Vector3(tip.transform.position.x, newTipYPos, tip.transform.position.z);
            float newHeight = tip.transform.localPosition.y - (butt.transform.localPosition.y + butt.GetComponent<SpriteRenderer>().sprite.bounds.size.y * butt.transform.localScale.y);
            float newYScale = (newHeight + y_offset) / body.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            body.transform.localScale = new Vector3(body.transform.localScale.x, newYScale, body.transform.localScale.z);
        }
    }
}
