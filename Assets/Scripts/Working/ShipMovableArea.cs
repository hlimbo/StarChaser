using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Area the ship can move around is defined to be the entire canvas background image (e.g. entire screen size of mobile device)
//Note: pointerdown event is detected via raycast on the contents of the CameraCanvas game object through a Graphics Raycaster component
public class ShipMovableArea : MonoBehaviour {

    [Tooltip("The player ship")]
    public GameObject ship;
    [Tooltip("Area in which player vessel cannot move towards or inside of")]
    public GameObject abilityBar;
    public GameObject pausePanel;
    public GameObject pauseButton;

    private RectTransform abilityBarRect;
    private RectTransform pauseButtonRect;
    private EventTrigger trigger;
    private Movement camMovement;
    [SerializeField]
    private int fingerMoveId = -1;

    void Awake()
    {
        ship = GameObject.FindWithTag("Player");
        pausePanel = GameObject.Find("PausePanel");
        abilityBar = GameObject.Find("AbilityBar");
        pauseButton = GameObject.Find("PauseButton");
    }

    // Use this for initialization
    void Start ()
    {
        trigger = GetComponent<EventTrigger>();
        abilityBarRect = abilityBar.GetComponent<RectTransform>();
        pauseButtonRect = pauseButton.GetComponent<RectTransform>();
        camMovement = Camera.main.GetComponent<Movement>();

        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, (data) => { fingerMoveId = (IsTouchingUI(data.position)) ? -1 : data.pointerId; });
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerUp, (data) => { fingerMoveId = -1; });
      
    }

    //stops movement when UI is clicked
    private bool IsTouchingUI(Vector2 touchPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(abilityBarRect, touchPos)
            || RectTransformUtility.RectangleContainsScreenPoint(pauseButtonRect, touchPos);
    }

    private void MoveShip(Vector2 cursorPos)
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(cursorPos);
        targetPos.z = 0f;
        float targetOffset = ship.GetComponent<Ship>().targetOffset;
        float speed = ship.GetComponent<Ship>().speed;
        Vector3 lockPosition = ship.transform.TransformPoint(Vector3.down * targetOffset);
        Vector3 delta = (targetPos - lockPosition).normalized;
        ship.transform.position = Vector3.MoveTowards(ship.transform.position, targetPos + Vector3.up * targetOffset, speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pausePanel.activeInHierarchy)
        {
            if (Input.touchSupported)
            {
                //only process movement if finger is on screen and finger is not on abilitybar
                if (Input.touchCount > 0 && fingerMoveId != -1)
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

                    MoveShip(fingerPos);
                }
                else //move ship along with camera if ship isn't being moved by player
                {
                    ship.transform.Translate(camMovement.transform.up * Time.deltaTime * camMovement.speed);
                }
            }
            else if (Input.mousePresent) // mouse controls ~ note: known to be glitchy when running on Unity Remote (won't detect touch input and defaults to this behaviour)
            {
                //0 = left mouse press
                //1 = right mouse press
                //2 = middle mouse press

                if (Input.GetMouseButton(0) && !IsTouchingUI(Input.mousePosition))
                {
                    MoveShip(Input.mousePosition);
                }
                else //move ship along with camera if ship isn't being moved by player
                {
                    ship.transform.Translate(camMovement.transform.up * Time.deltaTime * camMovement.speed);
                }
            }

        }
    }
}
