using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShieldBubble : MonoBehaviour{

    public GameObject shield;
    public GameObject playerShip;
    private CapsuleCollider2D shipHitbox;
    private CooldownTimer shieldCD;
    private EventTrigger trigger;
    private Animator shipAnim;
    private AudioSource audioSrc;

    public AudioClip shieldOnFX;
    public AudioClip shieldOffFX;

    void Start()
    {
        trigger = GetComponent<EventTrigger>();
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, Shield);
        shieldCD = GetComponent<CooldownTimer>();
        audioSrc = GetComponent<AudioSource>();
        shipHitbox = playerShip.GetComponent<CapsuleCollider2D>();
        shipAnim = playerShip.GetComponent<Animator>();
    }

    void Update()
    {
        if(!shieldCD.isAbilityActive && audioSrc.clip == shieldOnFX)
        {
            shield.SetActive(false);
            shipHitbox.enabled = true;
            shipAnim.SetBool("shieldActive", false);
            audioSrc.clip = shieldOffFX;
            audioSrc.Play();
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
            shipAnim.SetBool("shieldActive", true);
            audioSrc.clip = shieldOnFX;
            audioSrc.Play();
        }
    }
}
