using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Describes a portion of the boss's HP (e.g. head and hands have separate hp values that can be modified in BossManager script
[RequireComponent(typeof(Weakness))]
public class BossHP : MonoBehaviour,IWeaknessMessenger {

    public float hp;
    private BossManager bm;
    private CooldownTimer laserCD;
    [SerializeField]
    private bool isReceivingDamage = false;
    [SerializeField]
    private float elapsedDamageTime = 0f;

    void Awake()
    {
        bm = FindObjectOfType<BossManager>();
        GameObject LaserBar = GameObject.Find("LaserBar");
        laserCD = LaserBar.GetComponent<CooldownTimer>();
    }

    public void OnWeaknessReceived(Collider2D collision)
    {
        isReceivingDamage = true;
        StartCoroutine(CalculateDamageTime(collision));
    }


    IEnumerator CalculateDamageTime(Collider2D collision)
    {     
        float startTime = Time.time;
        PlayerDPS dps = collision.GetComponent<PlayerDPS>();
        //I need to interrupt the coroutine here when laser is no longer on this body part
        while (isReceivingDamage && laserCD.isAbilityActive)
        {
            yield return new WaitForSeconds(dps.frequency);
            elapsedDamageTime = Time.time - startTime;
        }
    }

    //apply final damage calculations made in CalculateDamagePerFrame ~ need to consider this design as well for enemies!
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Laser"))
        {
            isReceivingDamage = false;
            PlayerDPS dps = collision.GetComponent<PlayerDPS>();
            //adjust total damage received based on the time the laser spent on this boss body part
            float timeSpent = Mathf.Clamp(elapsedDamageTime, 0f, laserCD.abilityTimer.duration);
            float totalDamageReceived = dps.DamagePerFrame * (1f / dps.frequency) * timeSpent;

            Debug.Log(gameObject.name + " totalDamageReceived: " + totalDamageReceived);

            hp -= totalDamageReceived;
            bm.ApplyDamage(totalDamageReceived);
        }
    }
}
