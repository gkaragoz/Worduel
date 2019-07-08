using UnityEngine;

public class PreventMultiTouch : MonoBehaviour {

    [SerializeField]
    private bool _isMultiTouchEnabled = false;

    private void Awake() {
        Input.multiTouchEnabled = _isMultiTouchEnabled;
    }

}
