using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class PlayerLifeSystem : Weakness {

    public GameObject lifePanelGO;
    private PlayerLifeMessenger lifeMessenger;

    void Start()
    {
        Assert.IsNotNull(lifePanelGO, "Life Panel Game Object is null");
        lifeMessenger = lifePanelGO.GetComponent<PlayerLifeMessenger>();
    }

    void OnTriggerEnter2D(Collider2D collision)
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
                break;
            }
        }

        //kill player
        if(lifeMessenger.lives <= 0)
        {
            Destroy(gameObject);
        }
    }

}
