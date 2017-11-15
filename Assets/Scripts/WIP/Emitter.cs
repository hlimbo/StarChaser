using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {

    public GameObject projectilePrefab;
    public float fireRate = 1f;//number of projectiles fired per second

    public float speed;
    public float angle;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(FireProjectile());
	}

    IEnumerator FireProjectile()
    {
        while(true)
        {
            GameObject bullet = Instantiate<GameObject>(projectilePrefab,transform.position,Quaternion.identity);
            bullet.GetComponent<Projectile>().Init(speed, -transform.parent.GetComponent<Rigidbody2D>().rotation + angle);
            yield return new WaitForSeconds(1f / fireRate);
        }
    }
}
