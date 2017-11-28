using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotionController2 : MonoBehaviour {

    private Rigidbody2D rb;
    private EventTrigger trigger;
    private Movement camMovement;
    [SerializeField]
    private bool isFingerOnPivot;
    Vector3 newShipPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trigger = GetComponent<EventTrigger>();
        //data is PointerEventData type
        //onpointerdown is fired only when the cursor/finger is on the shipPivot for the first frame.
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, (data) => { isFingerOnPivot = true; });
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerUp, (data) => { isFingerOnPivot = false; });
        camMovement = Camera.main.GetComponent<Movement>();
    }

    void Update()
    {
        if(isFingerOnPivot)
        {
            Vector3 newShipPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newShipPos.z = 0f;
            transform.position = Vector3.MoveTowards(transform.position, newShipPos, 1.0f);
        }
        else //move ship along with camera if ship isn't being moved by player
        {
            transform.Translate(camMovement.transform.up * Time.deltaTime * camMovement.speed);
        }
    }
}
