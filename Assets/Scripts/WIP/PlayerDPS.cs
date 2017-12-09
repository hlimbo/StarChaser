using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Component is responsible for calculating DPS to be sent to an enemy ~ attach to ShipPivot
public class PlayerDPS : MonoBehaviour {

    public int dps;
    public float frequency = 0.02f;
    [SerializeField]
    private float damagePerFrame;//damage to be sent to enemy per frame (continuous)
    public float DamagePerFrame { get { return dps * frequency; } }

    private void Update()
    {
        damagePerFrame = DamagePerFrame;//to see what the dpf is in the editor
    }
}
