using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerLaser : MonoBehaviour {

    public GameObject laser;
    public GameObject shieldRef;
    public GameObject playerShip;

    private EnergyAbsorber EnergyScript;
    private CooldownTimer laserCD;
    private EventTrigger trigger;
    private Animator shipAnim;
    private AudioSource audioSrc;
    private Slider laserBar;

    void Start ()
    {
        trigger = GetComponent<EventTrigger>();
        laserCD = GetComponent<CooldownTimer>();
        audioSrc = GetComponent<AudioSource>();
        laserBar = GetComponent<Slider>();
        shipAnim = playerShip.GetComponent<Animator>();

        EnergyScript = shieldRef.GetComponent<EnergyAbsorber>();

        //drag and then remove finger off screen to fire laser
        //EventTriggerHelper.AddEvent(trigger, EventTriggerType.EndDrag, Laser);
        //tap the screen to fire laser
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, Laser);
    }
    
    void Update ()
    {
        if(!laserCD.isAbilityActive)
        {
            laser.SetActive(false);
            shipAnim.SetBool("laserActive", false);
        }
        else if(laserCD.isAbilityActive) //decrease charge over time
        {
            laserBar.value -= (laserCD.updateFrequency / laserCD.abilityTimer.duration) * Time.deltaTime;
            EnergyScript.charge = 0;
        }
    }

    //PointerDown ~ touch press to shoot laser
    public void Laser(PointerEventData data)
    {
        if(!laser.activeInHierarchy && !laserCD.isOnCooldown)
        {
            if (EnergyScript.charge == EnergyScript.maxCharge)
            {
                //Debug.Log("laser");
                shipAnim.SetBool("laserActive", true);
                laserCD.enabled = true;
                laser.SetActive(true);
                audioSrc.Play();
            }

        }
    }
}
