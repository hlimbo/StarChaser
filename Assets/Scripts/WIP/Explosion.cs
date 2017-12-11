using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public GameObject explosionPrefab;

    private void OnDisable()
    {
        Instantiate<GameObject>(explosionPrefab, transform.position, Quaternion.identity, null).GetComponent<ParticleSystem>().Play();
    }
}
