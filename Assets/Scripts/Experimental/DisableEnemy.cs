using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnemy : MonoBehaviour, IWeaknessMessenger
{
    public float duration = 3f;
    private bool isRunning = false;

    public void OnWeaknessReceived(Collider2D collision)
    {
        if(!isRunning)
            StartCoroutine(TheDelay());
    }

    IEnumerator TheDelay()
    {
        isRunning = true;
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
        isRunning = false;
    }
}
