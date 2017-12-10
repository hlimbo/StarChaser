using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPattern2 : MonoBehaviour {

    private MoveHead moveHead;
    private Emitter emitter;
    public float attackFrequency = 10f;
    private void Awake()
    {
        moveHead = GetComponent<MoveHead>();
        emitter = GetComponent<Emitter>();
        enabled = false;
    }

    void OnEnable()
    {
        StartCoroutine(AlternateAttacks());
    }

    void OnDisable()
    {
        StopCoroutine(AlternateAttacks());   
    }

    IEnumerator AlternateAttacks()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(attackFrequency);
            if (!moveHead.enabled)
            {
                moveHead.enabled = true;
                emitter.enabled = false;                
            }

            yield return new WaitForSeconds(moveHead.TotalDuration);
            emitter.enabled = true;
        }
    }




}
