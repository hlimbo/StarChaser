using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotionController2 : MonoBehaviour {

    private Rigidbody2D rb;
    private EventTrigger trigger;
    private Movement camMovement;
    [SerializeField]
    private bool isShipMoving;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trigger = GetComponent<EventTrigger>();
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.Drag, MoveShip);
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.EndDrag, EndDrag);
        camMovement = Camera.main.GetComponent<Movement>();
       
        //move ship when player is moving the ship so the ship stays within camera bounds
       // rb.MovePosition(rb.position + (Vector2)(camMovement.transform.up * Time.deltaTime * camMovement.speed));
    }

    //OnDrag
    public void MoveShip(PointerEventData data)
    {
        isShipMoving = true;
        rb.MovePosition(Camera.main.ScreenToWorldPoint(data.position));
    }

    public void EndDrag(PointerEventData data)
    {
        isShipMoving = false;
    }

    void Update()
    {
        if(!isShipMoving)
        {
            transform.Translate(camMovement.transform.up * Time.deltaTime * camMovement.speed);
        }
    }
}
