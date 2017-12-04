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
    //private BoxCollider2D box;
    private Animator shipAnim;
    private AudioSource audioSrc;
    private Slider laserBar;

    void Start ()
    {
        trigger = GetComponent<EventTrigger>();
        laserCD = GetComponent<CooldownTimer>();
        audioSrc = GetComponent<AudioSource>();
        laserBar = GetComponent<Slider>();
        // box = GetComponent<BoxCollider2D>();
        shipAnim = playerShip.GetComponent<Animator>();

        EnergyScript = shieldRef.GetComponent<EnergyAbsorber>();

        //drag and then remove finger off screen to fire laser
        //EventTriggerHelper.AddEvent(trigger, EventTriggerType.EndDrag, Laser);
        //tap the screen to fire laser
        EventTriggerHelper.AddEvent(trigger, EventTriggerType.PointerDown, Laser);

        //calculate the size of the bounding box collider to be as twice as wide as camera
        //{
        //    float camWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
        //    float camHeight = Camera.main.orthographicSize * 2f;
        //    box.size = new Vector2(camWidth * 2.5f, camHeight);
        //    box.offset = new Vector2(0f, camHeight / 2f);
        //    box.transform.localPosition = new Vector3(0f, 0.65f, 0f);
        //}
    }
    
    void Update ()
    {
        if(!laserCD.isAbilityActive)
        {
            laser.SetActive(false);
            shipAnim.SetBool("laserActive", false);
        }
    }

    IEnumerator DecreaseLaserCharge()
    {
        while(laserCD.abilityTimer.isTicking)
        {
            float f = laserCD.updateFrequency / (laserCD.abilityTimer.duration * 2f);
            laserBar.value -= f / laserCD.abilityTimer.duration;
            yield return new WaitForSeconds(f);
        }
        EnergyScript.charge = 0;
        yield return null;
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
                shipAnim.SetBool("laserActive", true);
                audioSrc.Play();
                StartCoroutine(DecreaseLaserCharge());
            }

        }
    }
}
