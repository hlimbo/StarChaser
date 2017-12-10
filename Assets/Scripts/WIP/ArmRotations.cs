using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotations : MonoBehaviour {

    public GameObject center;
    public float rotateSpeed = 120.0f;
    public float moveSpeed = 2.5f;
    public Quaternion originalRot;
    public Vector3 originalPos;
    public bool canRotate;

    private MoveArms moveArms;

    void Awake()
    {
        moveArms = transform.parent.GetComponent<MoveArms>();
        originalRot = transform.rotation;
        originalPos = transform.position;
        enabled = false;
    }

    void OnEnable ()
    {
        canRotate = true;
    }

    void OnDisable()
    {
        transform.rotation = originalRot;
        transform.position = originalPos;
    }
    
    // Update is called once per frame
    void Update () {

        if(moveArms.isMovingForwards)
        {
            canRotate = true;
            StartCoroutine(moveArms.ChangeMoveDirectionDelay());
        }

        if (moveArms.hasReturned)
        {
            moveArms.enabled = false;
            canRotate = false;
            transform.rotation = originalRot;
            transform.position = Vector3.MoveTowards(transform.position, originalPos, moveSpeed * Time.deltaTime);
        }

        if (canRotate)
        {
            transform.RotateAround(center.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        }
    }
}
