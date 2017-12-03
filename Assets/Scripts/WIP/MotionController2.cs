using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotionController2 : MonoBehaviour {

    private Rigidbody2D rb;
    private EventTrigger trigger;
    private Movement camMovement;

    [Tooltip("Measured in unity units per second")]
    public float speed = 10f;
    [Tooltip("Measures how many unity units finger/mouse cursor should be below ship. The higher the value the further away finger/mouse cursor is from ship. The lower the value, the closer finger/mouse cursor is from ship.")]
    public float targetOffset = 0.25f;

    [Tooltip("area in which player vessel cannot move towards or inside of")]
    public RectTransform abilityBar;
    [SerializeField]
    private int fingerMoveId = -1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camMovement = Camera.main.GetComponent<Movement>();
    }

    //stops movement when abilitybar is clicked
    private bool IsTouchingAbilityBar(Vector2 touchPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(abilityBar, touchPos);
    }

    void Update()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                //get correct finger associated with moving the ship
                Vector2 fingerPos = Vector2.zero;
                //do this to prevent out of bounds exception (e.g. press shield button first, then press on ship area to move, let go of ship button causes this exception to be thrown)
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    if (Input.GetTouch(i).fingerId == fingerMoveId)
                    {
                        fingerPos = Input.GetTouch(i).position;
                        break;
                    }
                }

                //process movement command if finger moving ship isn't on ability bar
                if (!IsTouchingAbilityBar(fingerPos))
                {
                    //move ship
                    Vector3 targetPos = Camera.main.ScreenToWorldPoint(fingerPos);
                    targetPos.z = 0f;
                    Vector3 lockPosition = transform.TransformPoint(Vector3.down * targetOffset);
                    Vector3 delta = (targetPos - lockPosition).normalized;
                    transform.position = Vector3.MoveTowards(transform.position, targetPos + Vector3.up * targetOffset, speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(camMovement.transform.up * Time.deltaTime * camMovement.speed);
                }
            }
            else //move ship along with camera if ship isn't being moved by player
            {
                transform.Translate(camMovement.transform.up * Time.deltaTime * camMovement.speed);
            }
        }
        else if(Input.mousePresent)//mouse controls
        {
            if(Input.GetMouseButton(0) && !IsTouchingAbilityBar(Input.mousePosition))
            {
                    //move ship
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
}
