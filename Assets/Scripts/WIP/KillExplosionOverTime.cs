using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillExplosionOverTime : MonoBehaviour {

    public float duration = 2f;

    private void OnEnable()
    {
        StartCoroutine(KillOverTime());
    }

    IEnumerator KillOverTime()
    {
        yield return new WaitForSeconds(duration);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
