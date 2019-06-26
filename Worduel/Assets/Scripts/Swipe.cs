using System;
using UnityEngine;

public class Swipe : MonoBehaviour {

    public Action onSwipeToLeft;
    public Action onSwipeToRight;
    public Action onSwipeToUp;
    public Action onSwipeToDown;
    public Action<Vector2> onDragging;

    [SerializeField]
    private float _sensivity = 125f;

    private bool _isTapping;
    private bool _isDragging;

    private Vector2 _startTouch;
    private Vector2 _swipeDelta;

    private void Update() {
        _isTapping = false;

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0)) {
            _isTapping = true;
            _isDragging = true;
            _startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0)) {
            _isDragging = false;
            Reset();
        }

        #endregion

#if !UNITY_EDITOR

        #region Mobile Inputs

        Touch touch = Input.GetTouch(0);
        if (Input.touchCount > 0) {
            if (touch.phase == TouchPhase.Began) {
                _isTapping = true;
                _isDraging = true;
                _startTouch = touch.position;
            } else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                _isDraging = false;
                Reset();
            }
        }

#endregion

#endif

        // Calculate the distance
        _swipeDelta = Vector2.zero;
        if (_isDragging) {
            if (Input.touchCount > 0) {
                _swipeDelta = Input.GetTouch(0).position - _startTouch;
            } else if (Input.GetMouseButton(0)) {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }

            onDragging?.Invoke(_swipeDelta);
        }

        // Did we cross the deadzone?
        if (_swipeDelta.magnitude > _sensivity) {
            // Which direction?
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y)) {
                // Left or right
                if (x < 0) {
                    onSwipeToLeft?.Invoke();
                } else {
                    onSwipeToRight?.Invoke();
                }
            } else {
                // Up or down
                if (y < 0) {
                    onSwipeToDown?.Invoke();
                } else {
                    onSwipeToUp?.Invoke();
                }
            }

            Reset();
        }
    }

    private void Reset() {
        _startTouch = _swipeDelta = Vector2.zero;
        _isDragging = false;
    }

}
