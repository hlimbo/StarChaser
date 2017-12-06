using UnityEngine.EventSystems;
using UnityEngine;

//use this interface for gameobjects that have a weakness component attached to them
public interface IWeaknessMessenger :  IEventSystemHandler
{
    void OnWeaknessReceived(Collider2D collision);
}
