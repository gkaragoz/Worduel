using UnityEngine;

public class Swipe : MonoBehaviour {

    private bool _isTapping;
    private bool _isSwipingToLeft;
    private bool _isSwipingToRight;
    private bool _isSwipingToUp;
    private bool _isSwipingToDown;
    private bool _isDraging;

    private Vector2 _startTouch;
    private Vector2 _swipeDelta;

    public Vector2 SwipeDelta { get { return _swipeDelta; } }
    public bool IsSwipingToLeft { get { return _isSwipingToLeft; } }
    public bool IsSwipingToRight { get { return _isSwipingToRight; } }
    public bool IsSwipingToUp { get { return _isSwipingToUp; } }
    public bool IsSwipingToDown { get { return _isSwipingToDown; } }

    private void Update() {
        _isTapping = _isSwipingToLeft = _isSwipingToRight = _isSwipingToUp = _isSwipingToDown = false;

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0)) {
            _isTapping = true;
            _isDraging = true;
            _startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0)) {
            _isDraging = false;
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
        if (_isDraging) {
            if (Input.touchCount > 0) {
                _swipeDelta = Input.GetTouch(0).position - _startTouch;
            } else if (Input.GetMouseButton(0)) {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
        }

        // Did we cross the deadzone?
        if (_swipeDelta.magnitude > 125) {
            // Which direction?
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y)) {
                // Left or right
                if (x < 0) {
                    _isSwipingToLeft = true;
                } else {
                    _isSwipingToRight = true;
                }
            } else {
                // Up or down
                if (y < 0) {
                    _isSwipingToDown = true;
                } else {
                    _isSwipingToUp = true;
                }
            }

            Reset();
        }
    }

    private void Reset() {
        _startTouch = _swipeDelta = Vector2.zero;
        _isDraging = false;
    }

}
