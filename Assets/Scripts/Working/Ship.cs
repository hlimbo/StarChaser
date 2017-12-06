using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains information about the ship's properties
public class Ship : MonoBehaviour {
    [Tooltip("Player Ship's Speed Measured in unity units per second")]
    public float speed = 10f;
    [Tooltip("Measures how many unity units finger/mouse cursor should be below ship. The higher the value the further away finger/mouse cursor is from ship. The lower the value, the closer finger/mouse cursor is from ship.")]
    public float targetOffset = 1f;
}
