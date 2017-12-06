﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

//must include lifePanelGO in scene for this script to function properly
public class PlayerLifeSystem : MonoBehaviour, IWeaknessMessenger {

    private PlayerLifeMessenger lifeMessenger;
    public float invicibleDuration = 3f;
    [Tooltip("Invincibility frequency used for animation (measured in seconds)")]
    public float frequency = 0.2f;
    [SerializeField]
    private bool isInvincible = false;
    private SpriteRenderer sr;
    private Weakness weakness;
    private CapsuleCollider2D hitCapsule;

    public GameObject sideAbsorberGO_L;
    public GameObject sideAbsorberGO_R;
    private BoxCollider2D sideAbsorber_L;
    private BoxCollider2D sideAbsorber_R;

    void Start()
    {
        lifeMessenger = FindObjectOfType<PlayerLifeMessenger>();
        Assert.IsNotNull(lifeMessenger, "PlayerLifeMessenger script reference is null");
        sr = GetComponent<SpriteRenderer>();
        weakness = GetComponent<Weakness>();
        hitCapsule = GetComponent<CapsuleCollider2D>();

        sideAbsorberGO_L = GameObject.Find("SideAbsorber_L");
        sideAbsorberGO_R = GameObject.Find("SideAbsorber_R");
        sideAbsorber_L = sideAbsorberGO_L.GetComponent<BoxCollider2D>();
        sideAbsorber_R = sideAbsorberGO_R.GetComponent<BoxCollider2D>();
    }

    public void OnWeaknessReceived(Collider2D collision)
    {
        if (!isInvincible)
        {
            //send message to player to lose life using an expression lambda
            //my guess here is that x represents the class that implemented the function LoseLife
            //and y represents the BaseEventData which is null
            ExecuteEvents.Execute<ILifeMessenger>(lifeMessenger.gameObject, null, (x, y) => x.LoseLife());
            //kill player
            if (lifeMessenger.lives <= 0)
                Destroy(gameObject);
            else
                StartCoroutine(BeginInvincibilityAnimation());
        }

    }

    IEnumerator BeginInvincibilityAnimation()
    {
        float startTime = Time.time;
        float elapsedTime = Time.time - startTime;
        isInvincible = true;
        hitCapsule.enabled = false;
        sideAbsorber_L.enabled = sideAbsorber_R.enabled = false;//side bullet absorbers shouldn't be enabled when invincible.. that would be too op..
        while (elapsedTime < invicibleDuration)
        {
            sr.enabled = !sr.enabled;
            elapsedTime = Time.time - startTime;
            yield return new WaitForSeconds(frequency);
        }
        isInvincible = false;
        hitCapsule.enabled = true;
        sideAbsorber_L.enabled = sideAbsorber_R.enabled = true;
        sr.enabled = true;
        yield return null;
    }

}
