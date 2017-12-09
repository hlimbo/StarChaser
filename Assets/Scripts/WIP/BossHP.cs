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
        PlayerDPS dps = collision.GetComponent<PlayerDPS>();
        float bodyPartHP = hp;//I have to cache the value here to compare its modified hp to the expected hp count after laser stops hitting body part
        float startTime = Time.time;
        float deltaTime = 0f;
        float realTimeDamage = 0f;
        //I need to interrupt the coroutine here when laser is no longer on this body part
        while (isReceivingDamage && laserCD.isAbilityActive)
        {
            bodyPartHP -= dps.maxDPS * deltaTime;
            if (bodyPartHP <= 0f)//determine if body part is dead
            {
                gameObject.SetActive(false);
                yield break;
            }

            //calculates approxmiate total damage dealt to this gameObject every coroutine 
            realTimeDamage += dps.maxDPS * deltaTime;
            float startDeltaTime = Time.time;
            //Does not guarantee that the time it will take to resume this coroutine will be exactly dps.frequency
            yield return new WaitForSeconds(dps.frequency); 
            deltaTime = Time.time - startDeltaTime;
            elapsedDamageTime = Time.time - startTime;
        }

        Debug.Log("BodyPartHP: " + bodyPartHP);
        Debug.Log("realTimeDamage: " + realTimeDamage);
    }


    //apply final damage calculations made in CalculateDamageTime ~ need to consider this design as well for enemies!
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Laser"))
        {
            //calculate final damage laser has dealt to this gameObject
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
}
