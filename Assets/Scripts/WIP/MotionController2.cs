using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotionController2 : MonoBehaviour {

    private Rigidbody2D rb;
    private EventTrigger trigger;
    private Movement camMovement;
    [SerializeField]
    private bool isFingerOnPivot;

    [Tooltip("Measured in unity units per second")]
    public float speed = 10f;
    [Tooltip("Measures how many unity units finger/mouse cursor should be below ship. The higher the value the further away finger/mouse cursor is from ship. The lower the value, the closer finger/mouse cursor is from ship.")]
    public float targetOffset = 0.25f;

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
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0f;
            Vector3 lockPosition = transform.TransformPoint(Vector3.down * targetOffset);
            Vector3 delta = (targetPos - lockPosition).normalized;
            transform.position = Vector3.MoveTowards(transform.position, targetPos + Vector3.up * targetOffset, speed * Time.deltaTime);
        }
        else //move ship along with camera if ship isn't being moved by player
        {
            transform.Translate(camMovement.transform.up * Time.deltaTime * camMovement.speed);
        }
    }
}
