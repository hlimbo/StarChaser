using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyAbsorber : MonoBehaviour
{
    public string tagToAbsorb;
    public int charge;
    public int maxCharge;
    public Slider laserBar;

    float currentChargePercent;
    float targetChargePercent;

    // Use this for initialization
    void Start()
    {
        charge = 0;
        currentChargePercent = targetChargePercent = 0f;
    }

    void Update()
    {
        if(currentChargePercent < targetChargePercent)
        {
            //smoothly move laser bar up to current charge
            currentChargePercent += ((float)charge / maxCharge) * Time.deltaTime;
            laserBar.value = currentChargePercent;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("collision");
        if (tagToAbsorb.Equals(collider.tag))
        {
            if (charge + 1 <= maxCharge)
            {
                // float oldValue = (float)charge / maxCharge;
                currentChargePercent = (float)charge / maxCharge;
                targetChargePercent = (float)(charge + 1) / maxCharge;
                //laserBar.value = newValue;
                ++charge;
            }
        }
    }

    

}
