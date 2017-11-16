using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShieldBubble : MonoBehaviour{

    public GameObject shield;
    private CooldownTimer shieldCD;
    private EventTrigger trigger;

    void Start()
    {
        trigger = GetComponent<EventTrigger>();
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, Shield);
        shieldCD = GetComponent<CooldownTimer>();
    }

    void Update()
    {
        if(!shieldCD.isAbilityActive)
        {
            shield.SetActive(false);
        }
    }

    public void Shield(PointerEventData data)
    {
        if (!shield.activeInHierarchy && !shieldCD.isOnCooldown)
        {
            Debug.Log("shield");
            shieldCD.enabled = true;
            shield.SetActive(true);
        }
    }
}
