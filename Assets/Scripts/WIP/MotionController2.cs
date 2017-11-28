using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotionController2 : MonoBehaviour {

    private Rigidbody2D rb;
    private EventTrigger trigger;
    private Movement camMovement;
    [SerializeField]
    private bool isFingerOnPivot;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trigger = GetComponent<EventTrigger>();
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, FingerDown);
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerUp, FingerUp);
        camMovement = Camera.main.GetComponent<Movement>();
    }

    public void FingerDown(PointerEventData data)
    {
        Debug.Log("Finger down");
        isFingerOnPivot = true;
    }

    public void FingerUp(PointerEventData data)
    {
        isFingerOnPivot = false;
    }

    void Update()
    {
        if (isFingerOnPivot)
        {
            //this will jitter
            rb.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else
        {
            transform.Translate(camMovement.transform.up * Time.deltaTime * camMovement.speed);
        }
    }
}
