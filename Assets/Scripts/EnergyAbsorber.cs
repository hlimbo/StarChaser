using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyAbsorber : MonoBehaviour
{

    public int charge;
    public int maxCharge;

    // Use this for initialization
    void Start()
    {
        charge = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D bullets)
    {
        if (charge < maxCharge)
            charge++;

    }

}
