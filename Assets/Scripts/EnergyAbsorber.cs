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

    // Use this for initialization
    void Start()
    {
        charge = 0;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("collision");
        if (tagToAbsorb.Equals(collider.tag))
        {
            if (charge + 1 <= maxCharge)
            {
               // float oldValue = (float)charge / maxCharge;
                float newValue = (float)(charge + 1) / maxCharge;
                laserBar.value = newValue;
                ++charge;
            }
        }
    }

    

}
