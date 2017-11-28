using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyAbsorber : MonoBehaviour
{

    public int charge;
    public int maxCharge;
    public Slider laserBar;

    // Use this for initialization
    void Start()
    {
        charge = 0;
    }

    void OnTriggerEnter2D(Collider2D bullets)
    {
        Debug.Log("collision");
        if (charge < maxCharge)
            charge++;

        laserBar.value = (float)charge / maxCharge;
    }

    

}
