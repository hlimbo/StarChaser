using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : Explosion {

    private BossManager bm;
    private void Awake()
    {
        bm = GetComponent<BossManager>();
    }

    private void OnDisable()
    {
        GameObject explosion = Instantiate<GameObject>(explosionPrefab, transform.position, Quaternion.identity, null);
        explosion.GetComponent<ParticleSystem>().Play();
    }


}
