using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerLaser : MonoBehaviour {

    public GameObject laser;
    public GameObject playerShip;

    private EnergyAccumulator energyAccum;
    private CooldownTimer laserCD;
    private EventTrigger trigger;
    private Animator shipAnim;
    private AudioSource audioSrc;
    private Slider laserBar;

    void Awake()
    {
        laser = GameObject.Find("LaserPivot");
        playerShip = GameObject.Find("playerShip");
    }

    void Start ()
    {
        trigger = GetComponent<EventTrigger>();
        laserCD = GetComponent<CooldownTimer>();
        audioSrc = GetComponent<AudioSource>();
        laserBar = GetComponent<Slider>();
        shipAnim = playerShip.GetComponent<Animator>();
        energyAccum = playerShip.GetComponent<EnergyAccumulator>();

        //drag and then remove finger off screen to fire laser
        //EventTriggerHelper.AddEvent(trigger, EventTriggerType.EndDrag, Laser);
        //tap the screen to fire laser
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, Laser);
    }
    
    void Update ()
    {
        if(!laserCD.isAbilityActive)
        {
            if (laserCD.isOnCooldown)
            {
                energyAccum.charge = 0; //reset charge
            }
            laser.SetActive(false);

            if (shipAnim != null) // turns to null when ship gameobject is destroyed
                shipAnim.SetBool("laserActive", false);
        }
        else if(laserCD.isAbilityActive) //decrease charge over time
        {
            laserBar.value -= (laserCD.updateFrequency / laserCD.abilityTimer.duration) * Time.deltaTime;
        }

    }

    //PointerDown ~ touch press to shoot laser
    public void Laser(PointerEventData data)
    {
        if(!laser.activeInHierarchy && !laserCD.isOnCooldown)
        {
            if (energyAccum.charge == energyAccum.maxCharge)
            {
                //Debug.Log("laser");
                shipAnim.SetBool("laserActive", true);
                laserCD.enabled = true;
                laser.SetActive(true);
                audioSrc.Play();
            }

        }
    }

    public void Laser()
    {
        if (!laser.activeInHierarchy && !laserCD.isOnCooldown)
        {
            if (energyAccum.charge == energyAccum.maxCharge)
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
