using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class PlayerLifeSystem : Weakness {

    public GameObject lifePanelGO;
    private PlayerLifeMessenger lifeMessenger;

    public float invicibleDuration = 3f;
    [Tooltip("Invincibility frequency used for animation (measured in seconds)")]
    public float frequency = 0.2f;
    [SerializeField]
    private bool isInvincible = false;
    private SpriteRenderer sr;

    void Start()
    {
        Assert.IsNotNull(lifePanelGO, "Life Panel Game Object is null");
        lifeMessenger = lifePanelGO.GetComponent<PlayerLifeMessenger>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInvincible)
        {
            foreach (string weaknessTag in weaknessTags)
            {
                if (collision.tag.Equals(weaknessTag))
                {
                    Debug.Log("player weakness touched: " + collision.tag);
                    //send message to player to lose life using an expression lambda
                    //my guess here is that x represents the class that implemented the function LoseLife
                    //and y represents the BaseEventData which is null
                    ExecuteEvents.Execute<ILifeMessenger>(lifePanelGO, null, (x, y) => x.LoseLife());
                    Destroy(collision.gameObject);
                    StartCoroutine(BeginInvincibilityAnimation());
                    break;
                }
            }
        }

        //kill player
        if (lifeMessenger.lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BeginInvincibilityAnimation()
    {
        float startTime = Time.time;
        float elapsedTime = Time.time - startTime;
        isInvincible = true;
        while (elapsedTime < invicibleDuration)
        {
            sr.enabled = !sr.enabled;
            elapsedTime = Time.time - startTime;
            yield return new WaitForSeconds(frequency);
        }
        isInvincible = false;
        sr.enabled = true;
        yield return null;
    }

}
