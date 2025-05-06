using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class InputController : MonoBehaviour
    {
        public bool IsHolding()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved;
            }
            return false;
        }

        public bool IsReleased()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return touch.phase == TouchPhase.Ended;
            }
            return false;
        }
    }
}