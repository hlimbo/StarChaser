using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public GameObject explosionPrefab;

    private void OnDisable()
    {
        GameObject explosion = Instantiate<GameObject>(explosionPrefab, transform.position, Quaternion.identity, null);
        explosion.GetComponent<ParticleSystem>().Play();
    }
}
