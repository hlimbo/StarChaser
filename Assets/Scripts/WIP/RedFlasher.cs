using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this script to enemies that should flash red when being hit by the laser
[RequireComponent(typeof(SpriteRenderer))]
public class RedFlasher : MonoBehaviour {

    private SpriteRenderer sr;
    [SerializeField]
    Color originalColor;

    [SerializeField]
    bool isFlashing = false;

    public Color targetColor;

    public float frequency = 0.5f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    IEnumerator DamageEffect()
    {
        while(isFlashing)
        {
            sr.color = Color.Lerp(originalColor,targetColor, Mathf.PingPong(Time.time, 1));
            yield return new WaitForSeconds(frequency);
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Laser"))
        {
            if (gameObject.layer == (int)BossManager.HitLayer.DEFAULT)
            {
                isFlashing = true;
                StartCoroutine(DamageEffect());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Laser"))
        {
            if (gameObject.layer == (int)BossManager.HitLayer.DEFAULT)
            {
                isFlashing = false;
                StopCoroutine(DamageEffect());
                sr.color = originalColor;
            }
        }
    }
}
