using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to count how long an ability can be cast again (measured in seconds)
//can be used for enemy or player ability cooldowns such as shield bubbble

//this component should be initially disabled and becomes activated
//by another script that requires its functionality.. e.g. player input component triggers the cooldown when shield effect activates with key press s.
public class CooldownTimer : MonoBehaviour
{
    [System.Serializable]
    public struct Timer
    {
        public float duration;
        public float startTime;
        public float elapsedTime;
        public bool isTicking;
    }

    public Timer cdTimer;
    public Timer abilityTimer;
    [SerializeField]
    private float totalDuration;
    public float updateFrequency = 1.0f;//how often the cooldown timer counts down by

    //I'm using enabled flag because it is convenient to see when this component is on or off cooldown in the editor
    public bool isOnCooldown { get { return cdTimer.isTicking; } private set { cdTimer.isTicking = value; } }
    public bool isAbilityActive { get { return abilityTimer.isTicking; } private set { abilityTimer.isTicking = value; } }

    void Awake()
    {
        totalDuration = abilityTimer.duration + cdTimer.duration;
        //disable this script from calling Update() every frame
        this.enabled = false;
    }

    void OnEnable()
    {
        //don't re-enable ability if ability is already active or on cooldown
        if (isAbilityActive || isOnCooldown)
            this.enabled = false;
        else
            StartCoroutine(AbilityCountdown());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator AbilityCountdown()
    {
        abilityTimer.startTime = Time.time;
        abilityTimer.elapsedTime = Time.time - abilityTimer.startTime;
        abilityTimer.isTicking = true;
        while (abilityTimer.elapsedTime < abilityTimer.duration)
        {
            yield return new WaitForSeconds(updateFrequency);
            abilityTimer.elapsedTime = Time.time - abilityTimer.startTime;
        }
        abilityTimer.isTicking = false;
        yield return CDCountdown();
    }

    IEnumerator CDCountdown()
    {
        cdTimer.startTime = Time.time;
        cdTimer.elapsedTime = Time.time - cdTimer.startTime;
        cdTimer.isTicking = true;
        while (cdTimer.elapsedTime < cdTimer.duration)
        {
            yield return new WaitForSeconds(updateFrequency);
            cdTimer.elapsedTime = Time.time - cdTimer.startTime;
        }
        cdTimer.isTicking = false;
        this.enabled = false;
        yield return null;
    }
}
