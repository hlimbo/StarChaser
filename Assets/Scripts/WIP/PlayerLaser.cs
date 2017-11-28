﻿using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerLaser : MonoBehaviour {

    public GameObject laser;
    public GameObject shieldRef;
    public EnergyAbsorber EnergyScript;
    private CooldownTimer laserCD;
    private EventTrigger trigger;
    private BoxCollider2D box;

    void Start ()
    {
        trigger = GetComponent<EventTrigger>();
        laserCD = GetComponent<CooldownTimer>();
        box = GetComponent<BoxCollider2D>();

        EnergyScript = shieldRef.GetComponent<EnergyAbsorber>();

        //drag and then remove finger off screen to fire laser
        //EventTriggerHelper.AddEvent(trigger, EventTriggerType.EndDrag, Laser);
        //tap the screen to fire laser
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, Laser);

        //calculate the size of the bounding box collider to be as twice as wide as camera
        {
            float camWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
            float camHeight = Camera.main.orthographicSize * 2f;
            box.size = new Vector2(camWidth * 2.5f, camHeight);
            box.offset = new Vector2(0f, camHeight / 2f);
            box.transform.localPosition = new Vector3(0f, 0.65f, 0f);
        }
    }
    
    void Update ()
    {
        if(!laserCD.isAbilityActive)
        {
            laser.SetActive(false);
        }
    }

    //PointerDown ~ touch press to shoot laser
    public void Laser(PointerEventData data)
    {
        if(!laser.activeInHierarchy && !laserCD.isOnCooldown)
        {
            if (EnergyScript.charge == EnergyScript.maxCharge)
            {
                Debug.Log("laser");
                laserCD.enabled = true;
                laser.SetActive(true);
            }

            //laser orientation ~ may scrap
           // Vector2 tip = transform.position;
           // Vector2 targetPos = Camera.main.ScreenToWorldPoint(data.position);
           // laser.transform.rotation = Quaternion.FromToRotation(Vector3.up, (Vector3)(targetPos - tip));

        }
    }
}