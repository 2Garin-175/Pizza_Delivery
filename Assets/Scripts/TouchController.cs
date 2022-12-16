using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private bool inTouch;
    private Vector3 touchPosicin;

    [SerializeField] private Bicycle bicycle;

#if !UNITY_EDITOR
    private void Update()
    {
        if (Input.touchCount > 0 && !inTouch)
        {
            inTouch = true;
            touchPosicin = Input.touches[0].position;
        }
        else if (Input.touchCount > 0 && inTouch)
        {
            if (touchPosicin.y < Input.touches[0].position.y)
            {
                bicycle.MoveForward();
            }
        }
        else
        {
            inTouch = false;
        }
    }
#endif

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetMouseButton(0) &&!inTouch)
        {
                inTouch = true;
                touchPosicin = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0) && inTouch)
        {
            if (touchPosicin.y < Input.mousePosition.y)
            {
                bicycle.MoveForward();
            }
        }
        else
        {
            inTouch = false;
        }

    }
#endif
}
