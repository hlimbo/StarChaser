using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFlash : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    Color originalColor;

    [SerializeField]
    bool isFlashing = false;

    public Color targetColor;

    public float frequency = 0.5f;

    void Awake()
    {
        sr = GameObject.Find("playerShip").GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    IEnumerator DamageEffect()
    {
        while(isFlashing)
        {
            Debug.Log("flashing");
            sr.color = Color.Lerp(originalColor,targetColor, Mathf.PingPong(Time.time, 1));
            yield return new WaitForSeconds(frequency);
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Bullet"))
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
        if(collision.tag.Equals("Bullet"))
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
