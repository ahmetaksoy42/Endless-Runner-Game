using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class SwipeManager : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;

    [SerializeField]public static bool jump, left, right, down;

    Vector2 startTouchPosition;
    Vector2 endTouchPosition;

    private void Start()
    {
        jump = left = right = down = false;
    }
    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener (OnSwipe);
    }

    private void OnSwipe(string swipe)
    {
        switch (swipe)
        {
            case "Left":
                left = true;
                jump = right = down = false;
                break;
            case "Right":
                right = true;
                jump = left = down = false;
                break;
            case "Up":
                jump = true;
                left = right = down = false;
                break;
            case "Down":
                down = true;
                jump = left = right = false;
                break;
        }
    }
    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }
    void Update()
    {
       
        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.deltaPosition.x > 100f)
            {
                right = true;
            }
            if (touch.deltaPosition.x < -100f)
            {
                left= true;
            }
            if (touch.deltaPosition.y < -100f)
            {
                down = true;
            }
            if (touch.deltaPosition.y > 100f)
            {
                jump = true;
            }
        }
        */

    }
}
