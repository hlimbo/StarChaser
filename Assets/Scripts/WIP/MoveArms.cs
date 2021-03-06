﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArms : MonoBehaviour {

    public float changeDirectionDelay;
    public float moveSpeed;
    public bool isMovingForwards = false;
    public Vector3 originalPos;

    //use this bool to check if arms moved back to its original position
    public bool hasReturned
    {
        get
        {
            return !isMovingForwards && ApproxEqual(transform.position, originalPos, .002f);
        }
    }

    void Awake()
    {
        originalPos = transform.position;
        enabled = false;
    }

    void OnEnable()
    {
        isMovingForwards = true;
        foreach(var child in transform.GetComponentsInChildren<ArmRotations>())
        {
            child.enabled = true;
        }
        StartCoroutine(ChangeMoveDirectionDelay());
    }

    void OnDisable()
    {
        foreach (var child in transform.GetComponentsInChildren<ArmRotations>())
        {
            child.enabled = false;
        }

    }

    void Update()
    {
        //stop moving backwards once the center position of the arms moves back to the original position.
        if (isMovingForwards)
            transform.Translate(transform.up * -1 * Time.deltaTime * moveSpeed, Space.Self);
        else
            transform.position = Vector3.MoveTowards(transform.position, originalPos, moveSpeed * Time.deltaTime);

        bool canDisable = true;
        foreach(var child in transform.GetComponentsInChildren<ArmRotations>())
        {
            if (child.transform.position != child.originalPos)
            {
                canDisable = false;
                break;
            }
        }
        if (hasReturned && canDisable)
            enabled = false;
    }

    IEnumerator ChangeMoveDirectionDelay()
    {
        yield return new WaitForSeconds(changeDirectionDelay);
        isMovingForwards = false;
        yield return null;
    }

    private bool ApproxEqual(Vector3 a,Vector3 b,float tolerance)
    {
        return AlmostEqual(a.x, b.x, tolerance) && AlmostEqual(a.y, b.y, tolerance) && AlmostEqual(a.z, b.z, tolerance);
    }

    private bool AlmostEqual(float a, float b, float epsilon)
    {
        return Mathf.Abs(a - b) < epsilon;
    }
}
