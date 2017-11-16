using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotionController2 : MonoBehaviour {

    private Rigidbody2D rb;
    private EventTrigger trigger;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trigger = GetComponent<EventTrigger>();
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.Drag, MoveShip);
    }

    //OnDrag
    public void MoveShip(PointerEventData data)
    {
        rb.MovePosition(Camera.main.ScreenToWorldPoint(data.position));
    }
}
