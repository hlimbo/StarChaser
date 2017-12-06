using UnityEngine;
using UnityEngine.EventSystems;

public class Weakness : MonoBehaviour {

    public string[] weaknessTags;

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(string weaknessTag in weaknessTags)
        {
            if (collision.tag.Equals(weaknessTag))
            {
                //Debug.Log("weakness touched: " + collision.tag);
                //Debug.Log(collision.gameObject.name)

                //if none of the gameobject's script can handle IWeaknessMessenger, destroy gameobject by default
                if (!ExecuteEvents.CanHandleEvent<IWeaknessMessenger>(gameObject))
                    Destroy(gameObject);
                else //implement your own functionality once weakness by some object is triggered
                    ExecuteEvents.Execute<IWeaknessMessenger>(gameObject, null, (x, y) => x.OnWeaknessReceived(collision));

                break;
            }
        }
    }
}
