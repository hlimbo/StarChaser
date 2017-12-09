using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotations : MonoBehaviour {

    public GameObject center;
    public float rotateSpeed = 120.0f;
    public Quaternion originalRot;
    public Vector3 originalPos;
    public bool canRotate;

    private MoveArms moveArms;

    // Use this for initialization
    void Start () {
        originalRot = transform.rotation;
        originalPos = transform.position;
        canRotate = true;
        moveArms = transform.parent.GetComponent<MoveArms>();
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
            canRotate = false;
            transform.rotation = originalRot;
            transform.position = Vector3.MoveTowards(transform.position, originalPos, rotateSpeed * Time.deltaTime);
        }

        if (canRotate)
        {
            transform.RotateAround(center.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        }
    }
}
