using System.Collections;
using UnityEngine;

public class Emitter : MonoBehaviour {

    [System.Serializable]
    public class EmitterOrigin {
        public GameObject projectilePrefab;
        public Vector3 offset;

        public float initDelay;

        [Tooltip("fireRate is projectiles fired per second from this emission point")]
        public float fireRate = 1.0f;

        public float angle = 180.0f;
        public float speed = 3.0f;

        [Tooltip("Only used if prefab contains Acceleration component.")]
        public float acceleration = 0.0f;

        [Tooltip("Only used if prefab contains Homing component.")]
        public float fuel = 2.0f;
        [Tooltip("Only used if prefab contains Homing component.")]
        public float unfocusRange = 1.125f;

        [Tooltip("Only used if prefab contains PredeterminedTurn component.")]
        public bool loopsTurning = false;

        [System.Serializable]
        public class Turn {
            public float time;
            public float angleChange;
        }

        [Tooltip("Only used if prefab contains PredeterminedTurn component.")]
        public Turn[] turns;
    }

    public EmitterOrigin[] emitters;

    // Use this for initialization
    void OnEnable ()
    {
        foreach (EmitterOrigin e in emitters) {
            StartCoroutine (FireProjectile (e));
        }
    }

    void Update ()
    {
        foreach (EmitterOrigin e in emitters) {
            if (e.initDelay > 0.0f)
                e.initDelay -= Time.deltaTime;
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator FireProjectile(EmitterOrigin e)
    {
        while(enabled)
        {
            if (e.initDelay <= 0.0f && e.fireRate > 0.0f && GetComponent<SpriteRenderer>().isVisible) {
                GameObject bullet = Instantiate<GameObject> (e.projectilePrefab, transform.position + e.offset, Quaternion.identity);
                bullet.GetComponent<Movement> ().speed = e.speed;
                bullet.transform.rotation = Quaternion.Euler (0.0f, 0.0f, e.angle);

                if (bullet.GetComponent<Acceleration> () != null) {
                    bullet.GetComponent<Acceleration> ().acceleration = e.acceleration;
                }

                if (bullet.GetComponent<Homing> () != null) {
                    bullet.GetComponent<Homing> ().fuel = e.fuel;
                    bullet.GetComponent<Homing> ().unfocusRange = e.unfocusRange;
                }

                if (bullet.GetComponent<PredeterminedTurn> () != null) {
                    bullet.GetComponent<PredeterminedTurn> ().repeating = e.loopsTurning;
                    bullet.GetComponent<PredeterminedTurn> ().turns = new PredeterminedTurn.Turn[e.turns.Length];
                    for (int i = 0; i < e.turns.Length; ++i) {
                        bullet.GetComponent<PredeterminedTurn> ().turns [i] = new PredeterminedTurn.Turn ();
                        bullet.GetComponent<PredeterminedTurn> ().turns [i].time = e.turns [i].time;
                        bullet.GetComponent<PredeterminedTurn> ().turns [i].angleChange = e.turns [i].angleChange;
                    }
                }
            }

            yield return new WaitForSeconds (1.0f / e.fireRate);
        }
    }
}

