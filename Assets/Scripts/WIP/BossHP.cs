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

    void Awake()
    {
        bm = FindObjectOfType<BossManager>();
        GameObject LaserBar = GameObject.Find("LaserBar");
        laserCD = LaserBar.GetComponent<CooldownTimer>();
    }

    public void OnWeaknessReceived(Collider2D collision)
    {
        StartCoroutine(ApplyDamagePerFrame(collision));
    }

    IEnumerator ApplyDamagePerFrame(Collider2D collision)
    {
        PlayerDPS dps = collision.GetComponent<PlayerDPS>();
        while (laserCD.isAbilityActive)
        {
            hp -= dps.DamagePerFrame;
            bm.ApplyDamage(dps.DamagePerFrame);
            yield return new WaitForSeconds(dps.frequency);
        }
        yield return null;
    }
}
