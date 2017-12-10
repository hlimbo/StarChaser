using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPattern1 : MonoBehaviour {

    private MoveArms moveArms;
    private Transform[] arms;

    public float attackFrequency = 10f;

    void Start()
    {
        moveArms = GetComponent<MoveArms>();
        arms = new Transform[2];
        for(int i = 0;i < transform.childCount; ++i)
        {
            arms[i] = transform.GetChild(i);
        }

        StartCoroutine(AlternateAttacks());
    }

    //every other attackFrequency seconds, enable moveArms component
    IEnumerator AlternateAttacks()
    {
        while (enabled)
        {
            float startTime = Time.time;
            float elapsedTime = Time.time - startTime;
            while (elapsedTime < attackFrequency)
            {
                yield return new WaitForSeconds(1f);
                elapsedTime = Time.time - startTime;
            }

            if (!moveArms.enabled)
            {
                ToggleArmEmitters(false);
                moveArms.enabled = true;
            }

            yield return new WaitForSeconds(moveArms.changeDirectionDelay * 4f);
            ToggleArmEmitters(true);

        }
    }

    private void ToggleArmEmitters(bool toggle)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            arms[i].GetComponent<Emitter>().enabled = toggle;
        }
    }
}
