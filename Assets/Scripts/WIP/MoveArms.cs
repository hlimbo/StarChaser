using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArms : MonoBehaviour {

    public float changeDirectionDelay;
    public float moveSpeed;
    public bool isMovingForwards = true;
    public Vector3 originalPos;

    //use this bool to check if arms moved back to its original position
    public bool hasReturned
    {
        get
        {
            return !isMovingForwards && originalPos == transform.position;
        }
    }

    void Awake()
    {
        originalPos = transform.position;
    }

    void Start()
    {
        StartCoroutine(ChangeMoveDirectionDelay());
    }

    void Update()
    {
        //stop moving backwards once the center position of the arms moves back to the original position.
        if (isMovingForwards)
            transform.Translate(transform.up * -1 * Time.deltaTime * moveSpeed, Space.World);
        else
            transform.position = Vector3.MoveTowards(transform.position, originalPos, moveSpeed * Time.deltaTime);
    }

    public IEnumerator ChangeMoveDirectionDelay()
    {
        float startTime = Time.time;
        float elapsedTime = Time.time - startTime;
        while (elapsedTime < changeDirectionDelay)
        {
            yield return new WaitForSeconds(1f);
            elapsedTime = Time.time - startTime;
        }
        isMovingForwards = false;
        yield return null;
    }
}
