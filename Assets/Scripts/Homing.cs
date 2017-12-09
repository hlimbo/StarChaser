using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {
    //private Movement movement;
    GameObject player;

    [Tooltip("The missile will stop homing if it runs out of fuel. Fuel drains based on time travelled.")]
    public float fuel = 5.0f;
    [Tooltip("At this distance from the target, the missile will unfocus and continue moving in a straight line.")]
    public float unfocusRange = 1.0f;

    // Use this for initialization
    void Start () {
        //movement = GetComponent<Movement> ();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    // Update is called once per frame
    void Update () {
        if (player == null || Vector3.Distance(transform.position, player.transform.position) <= unfocusRange) {
            fuel = 0.0f;
        }
        if (fuel > 0.0f) {
            fuel -= Time.deltaTime;
            transform.LookAt (player.transform);
            transform.up = player.transform.position - transform.position;
        }
    }

}
