using System;
using UnityEngine;

public class SwipeTest : MonoBehaviour {

    [SerializeField]
    private Swipe _swipe = null;

    private void Start() {
        _swipe.onDragging += OnDragging;
        _swipe.onSwipeToLeft += OnSwipeToLeft;
        _swipe.onSwipeToRight += OnSwipeToRight;
        _swipe.onSwipeToUp += OnSwipeToUp;
        _swipe.onSwipeToDown += OnSwipeToDown;
    }

    private void OnDragging(Vector2 delta) {
        Debug.Log(delta);
    }

    private void OnSwipeToLeft() {
        Debug.Log("Swiping to left");
    }

    private void OnSwipeToRight() {
        Debug.Log("Swiping to right");
    }

    private void OnSwipeToUp() {
        Debug.Log("Swiping to up");
    }

    private void OnSwipeToDown() {
        Debug.Log("Swiping to down");
    }

}
