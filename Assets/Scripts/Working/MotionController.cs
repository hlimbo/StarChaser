using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour {

    //how much the player vessel moves per frame [0,1]
    //e.g. 0 means no movement, 1 means move all the way to target location in 1 frame
    public float movePercent = 0.65f;
    //how sensitive is the device (e.g. phone) to changes in tracking touch positions
    private float tolerance = 0.0078125f;//2^-7
    //area in which player vessel cannot move towards or inside of
    public RectTransform abilityBar;

    void Update ()
    {
        //make sure you are running Unity Remote 5 with your phone plugged in via usb
        //to see the code run in action
        TouchMovement();
    }

    void TouchMovement()
    {
        if (Input.touchCount >= 1)
        {
            Touch firstTouch = Input.GetTouch(0);
            //cancel movement commands if first touch is inside of ability bar
            bool insideAbilityBar = RectTransformUtility.RectangleContainsScreenPoint(abilityBar, firstTouch.position);
            if(insideAbilityBar)
                return;

            Vector3 targetPos = Camera.main.ScreenToWorldPoint(firstTouch.position);
            if ((targetPos - transform.position).sqrMagnitude > tolerance)
            {
                switch (firstTouch.phase)
                {
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        //lockPosition is the position of where the touch location should snap to e.g. touch location snaps underneath the player vessel
                        Vector3 lockPosition = transform.TransformPoint(Vector3.down);
                        lockPosition = Vector3.Lerp(lockPosition, targetPos, movePercent);
                        lockPosition.z = 0.0f;
                        //may need to swap to using rigidbodies if we decide to check collision via triggers for moving game objects
                        transform.position = lockPosition + Vector3.up;
                        break;
                }
            }

        }
    }

}
