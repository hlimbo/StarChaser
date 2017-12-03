using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShieldBubble : MonoBehaviour{

    public GameObject shield;
    public GameObject playerShip;
    private CapsuleCollider2D shipHitbox;
    private CooldownTimer shieldCD;
    private EventTrigger trigger;

    void Start()
    {
        trigger = GetComponent<EventTrigger>();
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, Shield);
        shieldCD = GetComponent<CooldownTimer>();
        shipHitbox = playerShip.GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if(!shieldCD.isAbilityActive)
        {
            shield.SetActive(false);
            shipHitbox.enabled = true;
        }
    }

    public void Shield(PointerEventData data)
    {
        if (!shield.activeInHierarchy && !shieldCD.isOnCooldown)
        {
            Debug.Log("shield");
            shieldCD.enabled = true;
            shield.SetActive(true);
            shipHitbox.enabled = false;
        }
    }
}
