using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//This script will include a list of gameObject UI references
//that will stop the player from moving towards the gameObject UI reference when interacted on.
// (e.g. ship should not move towards the shield button when I press on it)
public class Untouchable : MonoBehaviour {

    public RectTransform[] untouchableAreas;

    //returns true if mouse press or finger press is on one of the rectTransform areas the ship should not move towards
    //false otherwise
    public bool IsTouchingUI(Vector2 touchPos)
    {
        foreach(RectTransform untouchableArea in untouchableAreas)
        {
            if (untouchableArea.gameObject.activeInHierarchy && RectTransformUtility.RectangleContainsScreenPoint(untouchableArea, touchPos))
            {
                Debug.Log("Untouchable: You touched: " + untouchableArea.gameObject.name);
                return true;
            }

        }

        return false;
    }
}
