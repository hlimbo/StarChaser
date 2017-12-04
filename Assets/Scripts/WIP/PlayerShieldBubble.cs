using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerShieldBubble : MonoBehaviour{

    public GameObject shield;
    public GameObject playerShip;
    private CapsuleCollider2D shipHitbox;
    private CooldownTimer shieldCD;
    private EventTrigger trigger;
    private Animator shipAnim;
    private AudioSource audioSrc;

    private Image bg_ready;
    private Image bg_cooldown;

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

        bg_cooldown = transform.GetChild(0).GetComponent<Image>();
        bg_ready = transform.GetChild(1).GetComponent<Image>();
    }

    void Update()
    {
        if(!shieldCD.isAbilityActive && audioSrc.clip == shieldOnFX)
        {
            shield.SetActive(false);
            shipHitbox.enabled = true;
            audioSrc.clip = shieldOffFX;
            audioSrc.Play();
        }

        if (shieldCD.isOnCooldown)
        {
            bg_ready.fillAmount += (shieldCD.updateFrequency / shieldCD.cdTimer.duration) * Time.deltaTime;
        }
        else if (!shieldCD.isAbilityActive && !shieldCD.isOnCooldown)
        {
            bg_ready.fillAmount = 1f;//take into account small numerical error when adding up fillamount back to 1
        }
    }

    public void Shield(PointerEventData data)
    {
        if (!shield.activeInHierarchy && !shieldCD.isOnCooldown)
        {
            //Debug.Log("shield");
            shieldCD.enabled = true;
            shield.SetActive(true);
            shipHitbox.enabled = false;
            audioSrc.clip = shieldOnFX;
            audioSrc.Play();
            bg_ready.fillAmount = 0f;
        }
    }
}
