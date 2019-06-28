using TMPro;
using UnityEngine;

public class KeyboardTextColors : MonoBehaviour {

    [SerializeField]
    private Color _pressedColor = Color.white;
    [SerializeField]
    private Color _defaultColor = Color.white;

    private TextMeshProUGUI _text;

    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void SetPressedColor() {
        _text.color = _pressedColor;
    }

    public void SetDefaultColor() {
        _text.color = _defaultColor;
    }

}
