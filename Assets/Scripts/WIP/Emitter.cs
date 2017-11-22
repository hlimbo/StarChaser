using System.Collections;
using UnityEngine;

public class Emitter : MonoBehaviour {

    [System.Serializable]
    public class EmitterOrigin {
        public GameObject projectilePrefab;
        public Vector3 offset;

        [Tooltip("fireRate is projectiles fired per second from this emission point")]
        public float fireRate = 1.0f;

        [Tooltip("'StraightLineBullet' prefab takes 2 args: angle, speed.\n" +
            "'HomingMissile' prefab takes 3 args: speed, fuel, maxDistance.\n" +
            "'ParametricProjectile' prefab takes ")]
        public float[] args;
    }

    public EmitterOrigin[] emitters;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(FireProjectile());
    }

    IEnumerator FireProjectile()
    {
        while(true)
        {
            foreach (EmitterOrigin e in emitters) {
                GameObject bullet = Instantiate<GameObject> (e.projectilePrefab, transform.position + e.offset, Quaternion.identity);
                bullet.GetComponent<Movement>().speed = e.args[1];
                bullet.transform.rotation = Quaternion.Euler (0.0f, 0.0f, e.args[0]);
                yield return new WaitForSeconds (1.0f / e.fireRate);
            }
        }
    }
}

