using UnityEngine;

public class SwipeTest : MonoBehaviour {

    [SerializeField]
    private Swipe _swipe = null;

    private void Update() {
        if (_swipe.IsSwipingToLeft) {
            Debug.Log("Swiping to left");
        }
        if (_swipe.IsSwipingToRight) {
            Debug.Log("Swiping to right");
        }
        if (_swipe.IsSwipingToUp) {
            Debug.Log("Swiping to up");
        }
        if (_swipe.IsSwipingToDown) {
            Debug.Log("Swiping to down");
        }
    }

}
