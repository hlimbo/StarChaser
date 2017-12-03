using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredeterminedTurn : MonoBehaviour {

    [System.Serializable]
    public class Turn {
        public float time;
        public float angleChange;
    }

    public Turn[] turns;

    [Tooltip("If set, completed turns are returned to end of turns queue.")]
    public bool repeating;

    private float elapsed = 0.0f;
    private int index = 0;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        elapsed += Time.deltaTime;
        if (turns.Length > index) {
            if (elapsed > turns[index].time) {
                elapsed -= turns[index].time;
                transform.Rotate (Vector3.forward * turns[index].angleChange);
                ++index;
                if (repeating && index == turns.Length) {
                    index = 0;
                }
            }
        }
    }
}
