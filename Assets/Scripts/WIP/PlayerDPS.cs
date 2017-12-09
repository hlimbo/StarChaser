using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Component is responsible for sending DPS information to affected enemies ~ attach to ShipPivot
public class PlayerDPS : MonoBehaviour {

    //maxDamage = maxDPS * (laserCD.abilityTimer.duration)
    public int maxDPS;
    public float frequency = 0.02f;//how much damage percentage to apply per frame
    [SerializeField]
    private float damagePerFrame;//damage to be sent to enemy per frame (continuous)
    public float DamagePerFrame { get { return maxDPS * frequency; } }

    private void Update()
    {
        damagePerFrame = DamagePerFrame;//to see what the dpf is in the editor
    }
}
