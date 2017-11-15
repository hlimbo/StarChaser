using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;//measured in unity units per second
    public float angle = 0f;//measured in degrees ~ which way projectile is oriented

    private Vector2 velocity;
    private Rigidbody2D rb;

    //used when instantiating this gameobject from an emitter component
    public void Init(float speed,float angle)
    {
        this.speed = speed;
        this.angle = angle;
    }

    void Start ()
    {
        //thanks unity for inverting x and y values :)
        velocity = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * speed;
        rb = GetComponent<Rigidbody2D>();
        rb.rotation = angle;
        transform.rotation = Quaternion.Euler(0f, 0f, -rb.rotation);
    }

    void Update()
    {
        rb.MovePosition(rb.position + (velocity * Time.deltaTime));
    }

}
