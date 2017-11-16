using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerLaser : MonoBehaviour {

    public GameObject laser;
    private CooldownTimer laserCD;
    private EventTrigger trigger;
    private BoxCollider2D box;

    void Start ()
    {
        trigger = GetComponent<EventTrigger>();
        laserCD = GetComponent<CooldownTimer>();
        box = GetComponent<BoxCollider2D>();
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, Laser);

        float camWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
        float camHeight = Camera.main.orthographicSize * 2f;
        box.size = new Vector2(camWidth * 2.5f, camHeight);
        box.offset = new Vector2(0f, camHeight / 2f);
    }
    
    void Update ()
    {
        if(!laserCD.isAbilityActive)
        {
            laser.SetActive(false);
        }
    }

    //PointerDown
    public void Laser(PointerEventData data)
    {
        if(!laser.activeInHierarchy && !laserCD.isOnCooldown)
        {
            Debug.Log("laser");
            laserCD.enabled = true;
            laser.SetActive(true);
            //todo: calculate laser orientation here
        }
    }
}
