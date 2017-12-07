using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveTrail : MonoBehaviour, IWeaknessMessenger {

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void OnWeaknessReceived(Collider2D collision) {
        Destroy (GetComponent<Movement> ());
        transform.localScale = Vector3.zero;
    }
}
