using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour {

    private EventTrigger trigger;
    private Image bg_ready;
    private Image bg_cooldown;
    private CooldownTimer cdTimer;

    // Use this for initialization
    void Start () {

        cdTimer = GetComponent<CooldownTimer>();
        trigger = GetComponent<EventTrigger>();
        bg_cooldown = transform.GetChild(0).GetComponent<Image>();
        bg_ready = transform.GetChild(1).GetComponent<Image>();
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, OnTouch);    
    }

    void Update()
    {
        if (cdTimer.isOnCooldown)
        {
            bg_ready.fillAmount += (cdTimer.updateFrequency / cdTimer.cdTimer.duration) * Time.deltaTime;
        }
    }

    public void OnTouch(PointerEventData data)
    {
        if (!cdTimer.isAbilityActive && !cdTimer.isOnCooldown)
        {
            cdTimer.enabled = true;
            Debug.Log("I touched: " + gameObject.name);
            bg_ready.fillAmount = 0f;
        }
    }
    
}
